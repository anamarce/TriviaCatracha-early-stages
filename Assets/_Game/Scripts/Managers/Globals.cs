using System.Text.RegularExpressions;

namespace x16
{
    //Most of the Enums of the Game
    public enum CharacterType { Player, Enemy }
    public enum LevelObjectType { Prefab, Movable, Text }
    public enum GameType { MINHITS, MAXHITS, CHALLENGE }

    public enum GraphicsQuality { VERYLOW, LOW, MEDIUM, HIGH, VERYHIGH };
    public enum RuntimePlatform { PC, IOS, ANDROID, WP8 }
    public enum CameraType { FIXED, FOLLOWUP }

    public enum InputMode { FORWARD, BACKWARD }

    public static class Globals
    {

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
        public class Tags
        {
            public static string Player = "Player";
            public static string Dodgeball = "Dodgeball";
            public static string Floor = "Floor";
            public static string WinZone = "WinZone";
            public static string DangerZone = "DangerZone";
            public static string Obstacle = "Obstacle";
            public static string Wall = "Wall";

    

        }

        public class MemekoStates
        {
            public static string Loading = "Loading";
            public static string Idle = "Idle";
            public static string Moving = "Moving";
            public static string Selecting = "Selecting";
            public static string OnTheHole = "OnTheHole";
            public static string Launching = "Launching";
            public static string OutOfBoard = "OutOfBoard";


        }
        public class Constants
        {
            public static string MinHitScene = "MinHitScene";
            public static string MaxHitScene = "MaxHitScene";
            public static string ChallengeScene = "ChallengeScene";

            public static string SelectPackScene = "SelectPackScene";
            public static string SelectModeScene = "SelectModeScene";
            public static string SelectLevelInPack = "SelectLevelInPack";
            public static string MainMenuScene = "MainMenuScene";
            public static string LoginScene = "LoginScene";
            public static string KiiScene = "KiiScene";

            public static readonly string[] PackNameArray = {"PACK-1","PACK-2","PACK-3","PACK-4"};
            public static int LevelsPerPack = 20;


            public static int LayerInputNumber = 10;
            public static float MaxScore = 5000F;
            public static float ScorePenaltyByHit = 100F;
            public static float ScorePenaltyByTurn = 300F;
            public static float ScoreBonusMaxHits = 500F;
            public static float ScoreBaseMaxHits = 500F;

            public static float SecondsOfGrace = 10F;
            public static int LimitSoundAlert = 5;


            public static string GlobalLeaderBoard = "GLOBAL";
            public static string Moneda_Dorada="Moneda_Dorada";
            public static string Moneda_Plateada = "Moneda_Plateada";

            public static string WonState = "Won";

            public static string LeaderBoardGlobal= "Global";
            public static string LeaderBoardPack1 = "pack1";
            public static string LeaderBoardPack2 = "pack2";
            public static string LeaderBoardPack3 = "pack3";
            public static string LeaderBoardPack4 = "pack4";
            public static string AfterWonState = "AfterWon";
            public static string StopRecordingState = "StopRecording";


        }
        public class GameEvents
        {
            // Build Strings with this format  TypeParameter, Name, Event
            // For example a player dies ...    Player_DieEvent
            // a player hits a obstacle ...  PlayerGameObject_HitObstacleEvent
            // receive 2 parameters, the player , the gameObject that he hits ..

            public static string DamageEvent_GameOjectFloat = "DamageEvent_GameOjectFloat";
            public static string MemekoLaunching = "MemekoLaunching";
            public static string PauseGame = "PauseGame";
            public static string ResumeGame = "ResumeGame";


            public static string RetryButtonPressed = "RetryButtonPressed";
            public static string BackButtonPressed = "BackButtonPressed";
        }

        public class InputEvents
        {
            // Build Strings with this format NameEvent_TypeParameter
            // For example a player dies ...    DieEvent_Player

            public static string TapEvent_InputData = "TapEvent_InputData";
            public static string LongTapEvent_InputData = "LongTapEvent_InputData";
            public static string InitPanEvent_InputData = "InitPanEvent_InputData";
            public static string MiddlePanEvent_InputData = "MiddlePanEvent_InputData";
            public static string EndPanEvent_InputData = "EndPanEvent_InputData";
            public static string EndMovingEvent = "EndMovingEvent";
            public static string EndZoomingEvent = "EndZoomingEvent";
            public static string MovingEvent = "MovingEvent";
            public static string InitMovingEvent = "InitMovingEvent";
            public static string InitZoomingEvent = "InitZoomingEvent";
            public static string ZoomingEvent_float = "ZoomingEvent_float";


        }

        public class GameValues
        {
      
            public static float BallMaxDragDistance = 8F; // Max distance to drag the arrow of Memeko
            public static float BallStopMagnitude = 0.2F;
            public static float BallColliderSize = 1.5F;
            public static float SceneFloorValueY = 0.169359F;
            public static float LoadingDelay = 3F; //Time necessary to load a level before goint to Ready
            public static float BulletTimeValue = 0.9F;

       


        }


    }
}

