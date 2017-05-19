using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.Res;
using System.IO;

namespace UnitTestApp2.MalProxy
{
    public static class TestUtils
    {
        public static string ReadFile(Context context, string filename)
        {
            AssetManager assets = context.Assets;
            string content;
            using (StreamReader sr = new StreamReader(assets.Open(filename)))
                content = sr.ReadToEnd();
            return content;
        }
    }
}