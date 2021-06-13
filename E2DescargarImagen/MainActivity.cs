using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Net;
using System;
using System.IO;
using System.Threading.Tasks;

namespace E2DescargarImagen
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ImageView Imagen;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var btnImagen = FindViewById<Button>(Resource.Id.btndescargar);
            Imagen = FindViewById<ImageView>(Resource.Id.imagen);
            btnImagen.Click += ArchivoImagen;
        }
        async void ArchivoImagen(object sender, EventArgs e)
        {
            var ruta = await DescargarImagen();
            var rutaImagen = Android.Net.Uri.Parse(ruta);
            Imagen.SetImageURI(rutaImagen);

        }
        public async Task<string>DescargarImagen()
        {
            var client = new WebClient();
            byte[] imagedata = await client.DownloadDataTaskAsync
                ("https://www.trecebits.com/wp-content/uploads/2019/04/11854.jpg");
            string documentspath = System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal);
            string localfilename = "foto1.jpg";
            string localpath = Path.Combine(documentspath, localfilename);
            File.WriteAllBytes(localpath, imagedata);
            return localpath;

        }
        
    }
}