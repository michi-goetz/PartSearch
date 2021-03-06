﻿using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Windows;
using PartSearch.Models;

namespace PartSearch
{
    public class SearchEngine
    {
        protected string _myURI;
        protected string _htmlText;
        protected string _backPartOfMyURI = ""; //falls der searchTerm inmitten der URI ist, kann hier der Teil hinter dem searchTerm gespeichert werden

        /**
         * Konstruktor
         * 
         * Ableitende Klassen müssen in ihren Konstruktoren ihre URI initialisieren!
         **/
        /*public SearchEngine()
        {
            //TODO: dies sollte in den abgeleiteten Klassen deffiniert werden
            _myURI = "http://www.google.de/#hl=de&q=";
        }*/

        /**
         * Gibt Host URL der Domain zurück
         **/
        public string URL
        {
            get
            {
                Uri URI = new Uri(_myURI);
                return URI.Host;
            }
        }

        /**
         * behandelt _backPartOfMyURI
         **/
        public string backPartOfMyURI
        {
            get
            {
                return _backPartOfMyURI;
            }
            set
            {
                _backPartOfMyURI = value;
            }
        }


        /**
         * Sucht nach searchTerm unter der initialisierten URI
         **/
        public void GetWebText(string searchTerm)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
                client.DownloadStringAsync(new Uri(_myURI + searchTerm + _backPartOfMyURI));
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message); //FIXME
            }
        }

        /**
         * Wird aufgerufen, wenn die aufgerufte Homepage antwortet
         **/
        protected void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // Make sure the process completed successfully
            if (e.Error == null)
            {
                // Use the result
                _htmlText = e.Result;
                MessageBox.Show(e.Result); //FIXME
            }
            else
            {
                //FIXME
                // WebException behandeln!
                MessageBox.Show("Error: " + e.Result);
            }
        }

        /**
         * Durchsucht den Webtext nach Bauteilen.
         * Wird von den abgeleitten Klassen mit override überschrieben.
         * 
         * Rückgabewert: null liste bei Fehler
         **/
        public virtual List<Product> GetParts()
        {
            List<Product> list = new List<Product>();
            return list;
        }
    }
}
