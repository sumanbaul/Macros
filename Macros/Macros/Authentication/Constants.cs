using System;
using System.Collections.Generic;
using System.Text;

namespace Macros.Authentication
{
    class Constants
    {
        public static string AppName = "OAuthNativeFlow";

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string iOSClientId = "1076172609577-k08vqrl89cqkrphelgcm6suq7d5o4s3o.apps.googleusercontent.com";
        public static string AndroidClientId = "1076172609577-k08vqrl89cqkrphelgcm6suq7d5o4s3o.apps.googleusercontent.com";

        public static string ClientSecret = "zwIbz1d8ia0K4COaQo4NQJDC";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "https://youtube.com";
        public static string AndroidRedirectUrl = "https://youtube.com";
    }
}
