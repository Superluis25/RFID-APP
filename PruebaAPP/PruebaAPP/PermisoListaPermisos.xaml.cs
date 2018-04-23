﻿using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServiceRest_RFID.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PruebaAPP
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PermisoListaPermisos : ContentPage
	{
        public Usuario UsuarioSrc { get; set; }
        public List<Permiso> LstPermisoSrc { get; set; }
        public String PathImagenRefresh
        {
            get
            {
                String nombre = "refresh.png";
                String ruta = "";
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        ruta = "" + nombre;
                        break;
                    case Device.iOS:
                        ruta = "Icons/" + nombre;
                        break;
                    case Device.UWP:
                        ruta = "Assets/Icons/" + nombre;
                        break;
                }
                return ruta;
            }
        }
        public String PathImagenAgregar
        {
            get
            {
                String nombre = "agregar.png";
                String ruta = "";
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        ruta = "" + nombre;
                        break;
                    case Device.iOS:
                        ruta = "Icons/" + nombre;
                        break;
                    case Device.UWP:
                        ruta = "Assets/Icons/" + nombre;
                        break;
                }
                return ruta;
            }
        }

        public PermisoListaPermisos(Usuario u)
        {
            try
            {
                UsuarioSrc = u;
                BindingContext = this;
                InitializeComponent();
                //CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
                //barraProgreso.ProgressTo(0.5, 500, Easing.Linear);
                //ObtenerPermisos();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    DisplayAlert("Alerta", ex.InnerException.Message, "Cerrar");
                }
                else
                {
                    DisplayAlert("Alerta", ex.Message, "Cerrar");
                }

            }

        }
        private async void ObtenerPermisos()
        {
            HttpClient client = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(5000)
            };
            try
            {
                await barraProgreso.ProgressTo(0.6, 500, Easing.Linear);
                var response = await client.GetStringAsync("http://201.172.20.116/permiso/" + UsuarioSrc.ID);
                await barraProgreso.ProgressTo(0.7, 500, Easing.Linear);
                LstPermisoSrc = JsonConvert.DeserializeObject<List<Permiso>>(response);
                LstPermisos.ItemsSource = LstPermisoSrc;
                await barraProgreso.ProgressTo(1, 500, Easing.Linear);
                if (LstPermisoSrc.Count > 0)
                {
                    LstPermisos.IsVisible = true;
                    absLayout.IsVisible = false;
                    absVacio.IsVisible = false;
                }
                else
                {
                    absLayout.IsVisible = false;
                    absVacio.IsVisible = true;
                }
            }
            catch (TaskCanceledException ex)
            {
                if (ex.Message != null)
                {
                    await DisplayAlert("Alerta", "Tarea cancelada por el sistema. " + ex.Message, "Cerrar");
                }
                else
                {
                    await DisplayAlert("Alerta", "Tarea cancelada por el sistema. " + ex.InnerException.Message, "Cerrar");
                }
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message != null)
                {
                    await DisplayAlert("Alerta", "No se pudo conectar al servidor. " + ex.Message, "Cerrar");
                }
                else
                {
                    await DisplayAlert("Alerta", "No se pudo conectar al servidor. " + ex.InnerException.Message, "Cerrar");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    await DisplayAlert("Alerta", ex.Message, "Cerrar");
                }
                else
                {
                    await DisplayAlert("Alerta", ex.InnerException.Message, "Cerrar");
                }
            }
        }

        private async void LstPermisos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Permiso p = (Permiso)e.SelectedItem;
            await Navigation.PushAsync(new EditarPermisos(p));
        }

        private async void tItemCrear_Activated(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditarPermisos(UsuarioSrc));
        }

        private async void tItemRefresh_Activated(object sender, EventArgs e)
        {
            await barraProgreso.ProgressTo(0.2, 500, Easing.Linear);
            absVacio.IsVisible = false;
            LstPermisos.IsVisible = false;
            absLayout.IsVisible = true;
            ObtenerPermisos();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await barraProgreso.ProgressTo(0.2, 500, Easing.Linear);
            absVacio.IsVisible = false;
            LstPermisos.IsVisible = false;
            absLayout.IsVisible = true;
            ObtenerPermisos();
        }
    }
}