//Created by: Matthew Humphrey
//Date: 11/3/17
//Purpose: To read a file in an app list view.

using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DunkinDonutApp
{
    [Activity(Label = "DunkinDonutApp", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : ListActivity
    {
        List<string> donutList = new List<string>(4900);
        List<string> fullList = new List<string>(4900);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.Main);
            StreamReader sr = new StreamReader(Assets.Open(@"dunkin_donuts.csv"));
            TextView tv = (TextView)FindViewById(Resource.Id.textView1);

            sr.ReadLine(); 
            while (sr.Peek() != -1)
            {
                fullList.Add(sr.ReadLine());
                fullList.Last().Replace("\\", " ");
                fullList.Last().Replace("\"", " ");
            }
            sr.Close();
            foreach (string full in fullList)
            {
                full.Replace("\\", "");
                full.Replace("\"", "");
                List<string> split = full.Split(',').ToList();
                donutList.Add(split[2] + " " + split[3] + "," + split[4] + "," + split[7]);
            }

            base.OnCreate(savedInstanceState);

            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.ListItem, donutList);
            
            ListView.TextFilterEnabled = true;

            ListView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
                string output;
                List<string> fullInfo = fullList[args.Position].Split(',').ToList();
                tv.Text = "";
                output = "Business Name: " + fullInfo[1] + "\nPostal Code: " + fullInfo[5];
                output += "\tZip: " + fullInfo[6] + "\nCounty: " + fullInfo[8];
                output += "\tArea Code: " + fullInfo[9] + "\nFIPS: " + fullInfo[10];
                output += "\tMSA: " + fullInfo[11] + "\nPSMA: " + fullInfo[12];
                output += "\nTZ: " + fullInfo[13] + "\tDST: " + fullInfo[14];
                output += "\nLatitude: " + fullInfo[15] + "\tLongitude: " + fullInfo[17];
                output += "\nWeb URL: " + fullInfo[19] + "\nAdditional Information: " + fullInfo[20];
                output += "\nPhone Number: " + fullInfo[21];
                tv.SetText(output.ToCharArray(),0,output.Length);
                tv.SetLines(11);
            };
        }

    }
}
