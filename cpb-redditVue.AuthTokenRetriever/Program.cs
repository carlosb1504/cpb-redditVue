﻿using Reddit.AuthTokenRetriever;
using System;
using System.Diagnostics;

namespace cpb_redditVue.AuthTokenRetriever
{
    class Program
    {
        // Change this to the path to your local web browser.  --Kris
        public const string BROWSER_PATH = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";

        static void Main(string[] args)
        {
            string appId = (args.Length >= 1 ? args[0] : null);
            string appSecret = (args.Length >= 2 ? args[1] : null);

            // Create a new instance of the auth token retrieval library.  --Kris
            AuthTokenRetrieverLib authTokenRetrieverLib = new AuthTokenRetrieverLib(appId, appSecret, 8080);

            // Start the callback listener.  --Kris
            authTokenRetrieverLib.AwaitCallback();
            Console.Clear();  // Gets rid of that annoying logging exception message generated by the uHttpSharp library.  --Kris

            // Open the browser to the Reddit authentication page.  Once the user clicks "accept", Reddit will redirect the browser to localhost:8080, where AwaitCallback will take over.  --Kris
            OpenBrowser(authTokenRetrieverLib.AuthURL());

            Console.ReadKey();  // Hit any key to exit.  --Kris

            authTokenRetrieverLib.StopListening();

            Console.WriteLine("Token retrieval utility terminated.");
        }

        public static void OpenBrowser(string authUrl = "about:blank")
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(authUrl);
                Process.Start(processStartInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // This typically occurs if the runtime doesn't know where your browser is.  Use BrowserPath for when this happens.  --Kris
                ProcessStartInfo processStartInfo = new ProcessStartInfo(BROWSER_PATH)
                {
                    Arguments = authUrl
                };
                Process.Start(processStartInfo);
            }
        }
    }
}
