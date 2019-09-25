using System;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

namespace DDNS
{
    public partial class DdnsForm : Form
    {
        public static string diction = Application.StartupPath + "..\\..\\..\\data";
        public static string path = diction + @"\data.db";
        public static string tableName = "ddns_info";
        public static List<string> para = new List<string> { "jj_id", "AccessKeyID", "AccessKeySecret", "RecordID" };
        WaterWave wave = new WaterWave();
        public DdnsForm()
        {
            InitializeComponent();
        }

        private void DdnsForm_Load(object sender, EventArgs e)
        {
            loadData();
            wave.SetImage(ref pictureBox1);
        }

        private void loadData()
        {
            //从数据库中取值
            DataTable table = SQLiteHelper.lodaData(path,tableName, para);
            //从返回的DataTable中取值
           if(table.Rows.Count<1)
            {
                return;
            }
           else
            {
                keyIdTextBox.Text = table.Rows[0]["AccessKeyID"].ToString();
                secretTextBox.Text = table.Rows[0]["AccessKeySecret"].ToString();
                recordIdTextBox.Text = table.Rows[0]["RecordID"].ToString();
            }

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            timerWave.Start();
            if (keyIdTextBox.Text.Length == 0 || secretTextBox.Text.Length == 0 || recordIdTextBox.Text.Length == 0)
            {
                MessageBox.Show("信息填写不完整！", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SQLiteHelper.deleteData(tableName, path);
                List<string> refValue = new List<string>();
                refValue.Add(keyIdTextBox.Text);
                refValue.Add(secretTextBox.Text);
                refValue.Add(recordIdTextBox.Text);
                bool inserResult = SQLiteHelper.addData(tableName, path, refValue);
                if (inserResult == true)
                {
                    resolutionTextBox.Text = "开始解析！" + "  时间：" + DateTime.Now.ToString();
                    timer.Start();

                }
            }

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string accesskeyId = keyIdTextBox.Text;
            string accessKeySecret = secretTextBox.Text;
            string recordId = recordIdTextBox.Text;

            string timestamp = DDNS.timeSpan();
            string signatureNonce = DDNS.uuNum();

            string localIP = DDNS.getLocalIP();
            string recordSignature = DDNS.signature(accessKeySecret,accesskeyId,recordId, signatureNonce, timestamp);
            string recordIP = DDNS.getRecord(accesskeyId,accessKeySecret,recordId,recordSignature,timestamp,signatureNonce);
            if(localIP!=recordIP)
            {
                timestamp = DDNS.timeSpan();
                signatureNonce = DDNS.uuNum();
                string updateSignature = DDNS.updateSignatureStr(localIP, accessKeySecret, accesskeyId,recordId, signatureNonce, timestamp);
                bool updateResult = DDNS.updateIP(localIP, accessKeySecret, accesskeyId, recordId, updateSignature, signatureNonce, timestamp);
                if(updateResult==true)
                {
                    resolutionTextBox.Text = "解析成功！"+"  时间："+DateTime.Now.ToString();
                }
                else
                {
                    resolutionTextBox.Text = "解析失败！" +" 时间："+ DateTime.Now.ToString();
                }
            }
            else
            {
                resolutionTextBox.Text = "正在解析！" + "  时间：" + DateTime.Now.ToString();
            }
        }

        private void timerWave_Tick(object sender, EventArgs e)
        {
            int x = new Random().Next(0,420);
            int y = new Random().Next(0,117);
            wave.SetWavePoint(x,y,20,-100);
        }
    }
}
