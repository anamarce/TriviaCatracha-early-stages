using System.Text.RegularExpressions;

namespace x16
{
    //Most of the Enums of the Game
  

    public enum GraphicsQuality { VERYLOW, LOW, MEDIUM, HIGH, VERYHIGH };
    public enum RuntimePlatform { PC, IOS, ANDROID, WP8 }
    public enum CameraType { FIXED, FOLLOWUP }


 

    public static class Globals
    {

		#region class utils
        public class utils
        {

            public static bool ValidUsername(string username)
            {

                Regex rex = new Regex(@"^[a-zA-Z]{1}[a-zA-Z0-9]{1}[a-zA-Z0-9\._\-]{1,13}$");


                if (rex.IsMatch(username))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool ValidPassword(string password)
            {

                Regex len = new Regex("^.{8,20}$");
                Regex num = new Regex("\\d");
                Regex alpha = new Regex("\\D");
                Regex special = new Regex(@"[><%#@\*\+\?\!\&\-]"); // Put  here more special characters

                if (len.IsMatch(password) && num.IsMatch(password) && alpha.IsMatch(password) )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
		#endregion


        public class Tags
        {
            public static string Player = "Player";
      }

        public class Constants
        {
            public static string MainScene = "MainScene";
          

            public static readonly string[] Languages = {"English","Espanol"};


            public static int MaxAnswers = 20;
            public static int IntervalAnswers = 5;
        }
        public class GameEvents
        {
            // Build Strings with this format  TypeParameter, Name, Event
            // For example a player dies ...    Player_DieEvent
            // a player hits a obstacle ...  PlayerGameObject_HitObstacleEvent
            // receive 2 parameters, the player , the gameObject that he hits ..

            public static string DamageEvent_GameOjectFloat = "DamageEvent_GameOjectFloat";
        
        }

      


    }
}

