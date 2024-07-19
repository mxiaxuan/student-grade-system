using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 學生成績系統
{
    public partial class Form1 : Form
    {
        //Class類別變數,寫在外面才能整個from都有該陣列
        string[] arrayStudentName;//學生姓名陣列
        int[] arrayStidentScore;//學生成績陣列
        string[] arrayTempStudentName;//學生暫存姓名陣列，需要暫存的原因為這些是傳址呼叫，會影響原有的資料，所以使用暫存
        int[] arrayTempStidentScore;//學生暫存成績陣列

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            arrayStudentName = new string[5];//index索引對應關係，key-value對應，key鍵
            arrayStudentName[0] = "王曉明";
            arrayStudentName[1] = "丁大維";
            arrayStudentName[2] = "柯憶純";
            arrayStudentName[3] = "許雅瑄";
            arrayStudentName[4] = "黃宇琳";

            arrayStidentScore = new int[5];//index索引對應關係，value值
            arrayStidentScore[0] = 50;
            arrayStidentScore[1] = 80;
            arrayStidentScore[2] = 60;
            arrayStidentScore[3] = 100;
            arrayStidentScore[4] = 70;

            arrayTempStudentName = new string[5];
            arrayTempStidentScore = new int[5];

            lbl成績總和.Text=string.Format("{0}", 計算總分(arrayStidentScore));
            lbl成績平均.Text= string.Format("{0}", 計算平均(arrayStidentScore));
        }

        int 計算總分(int[] myArrayScore)
        {
            int sum = 0;
            foreach(int score in myArrayScore)
            {
                sum += score;
            }

            return sum;
        }

        Double 計算平均(int[] myArrayScore)
        {
            Double avege = 0.0;
            foreach (Double avegei in myArrayScore)
            {
                avege = 計算總分(myArrayScore) / myArrayScore.Length   ;
            }

            return avege;
        }

        private void lbl查詢結果顯示_Click(object sender, EventArgs e)
        {

        }

        private void btn姓名排序_Click(object sender, EventArgs e)
        {
            Array.Copy(arrayStudentName,arrayTempStudentName,arrayStudentName.Length);
            //把原始的學生姓名檔跟長度複製到暫存檔去，以免下面列出所有學生資料時也被排序到了
            姓名排序(arrayTempStudentName);
        }

        void 姓名排序(string[] myArrayName)
        {
            System.Array.Sort(myArrayName);//符號0-9 a-z 中文由筆畫少到多

            string strMsg = "";
            
            for(int index =0; index<myArrayName.Length ;index ++)
            {
                strMsg += string.Format("姓名：{0}\n", myArrayName[index]);
            }
            lbl查詢結果顯示.Text = strMsg;
        }

        void 成績排序(string[] myArrayName,int[] myArrayScore) 
        {

            Array.Sort(myArrayScore,myArrayName);//鍵值對應排序
            //輸出成績與姓名，成績由高到低輸出

            string strMsg = "";

            for (int index = myArrayName.GetUpperBound(0); index >= 0; index--)
            //GetUpperBound取出陣列最大的索引值
            {
                strMsg += string.Format("姓名：{0} 成績:{1}\n", myArrayName[index], myArrayScore[index]);
                //{0},{1}不是陣列的[]，所以{0}為表參數列表中的第一个和第二个元素
            }
            lbl查詢結果顯示.Text = strMsg;
        }

        private void btn成績排序_Click(object sender, EventArgs e)
        {
            Array.Copy(arrayStudentName, arrayTempStudentName, arrayStudentName.Length);
            //把原始的學生姓名檔跟長度複製到暫存檔去，以免下面列出所有學生資料時也被排序到了
            Array.Copy(arrayStidentScore, arrayTempStidentScore, arrayStidentScore.Length);
            成績排序(arrayTempStudentName, arrayTempStidentScore);
        }

        private void btn列出所有學生資料_Click(object sender, EventArgs e)
        {
            列出所有資料();
        }
        void 列出所有資料()
        {
            String strMsg = "";

            int i = 0;
            foreach(String s in arrayStudentName)
            //約等於for迴圈，但可以取得所有元素 不能限制範圍
            {
                strMsg += $"索引值: {i} 姓名: {s} 成績:{arrayStidentScore[i]}\n";
                i += 1;
            }
            lbl查詢結果顯示.Text = strMsg;
        }

        private void btn姓名搜尋_Click(object sender, EventArgs e)
        {
            if (txt搜尋關鍵字.TextLength > 0)
            {
                int index, 名次 = 0;
                string strmsg = "搜尋結果\n-------------------\n";
                string strserchkey = txt搜尋關鍵字.Text;
                
                Array.Copy(arrayStudentName, arrayTempStudentName, arrayStudentName.Length);
                Array.Copy(arrayStidentScore, arrayTempStidentScore, arrayStidentScore.Length);

                //成績排序(arrayTempStudentName, arrayTempStidentScore);
                Array.Sort(arrayTempStidentScore, arrayTempStudentName);//鍵值對應排序

                index = Array.IndexOf(arrayTempStudentName, strserchkey);
                //Array.IndexOf()蒐集集合的內容
                //在strserchkey中的textbox裡中蒐集學生姓名資料

                if (index <= -1)
                //找不到對應的姓名，則index將為-1
                {
                    strmsg += "查無此人";
                }
                else
                {
                    名次 = arrayTempStudentName.Length - index;
                    //陣列長度減低分高分的索引
                    //假设数组已经按照成绩从高到低排序，而且没有重复的姓名。在这种情况下，index 表示的是该姓名在排序后数组中的位置，而数组的长度减去该位置即可得到名次。
                    strmsg += $"姓名:{arrayTempStudentName[index]}成績:{arrayTempStidentScore[index]}第{名次}名";

                }
                lbl查詢結果顯示.Text = strmsg;
            }
            else
            {
                MessageBox.Show("請輸入資料");
            }
        }

        private void txt搜尋關鍵字_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

