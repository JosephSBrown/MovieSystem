namespace MovieBooking
{
    class TextDisplay
    {
        //Create Title Screen  ASCII Art
        public static void HomeScreenText()
        {
            string one =   @$"   _________     __  ___   __  ___           _          ";
            string two =   @$"  / ____( _ )   /  |/  /  /  |/  /___ _   __(_)__  _____";
            string three = @$" / / __/ __ \/|/ /|_/ /  / /|_/ / __ \ | / / / _ \/ ___/";
            string four =  @$"/ /_/ / /_/  </ /  / /  / /  / / /_/ / |/ / /  __(__  ) ";
            string five =  @$"\____/\____/\/_/  /_/  /_/  /_/\____/|___/_/\___/____/  ";

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
        }

        //Create Movie Selection ASCII Art
        public static void MovieChoiceText()
        {
            string one =   @$"  ________                                       __  ___          _    ";
            string two =   @$" / ____/ /_  ____  ____  ________      ____ _   /  |/  /_____  __(_)__ ";
            string three = $@"/ /   / __ \/ __ \/ __ \/ ___ / _ \   / __ `/  / /|_/ / __  /| | / / _ \";
            string four =  @$"/ /___/ / / / /_/ / /_/ (__  )  __/   / /_/ /  / /  / / /_/ / |/ / /  __/";
            string five =  @$"\____/_/ /_/\____/\____/____/\___/    \__,_/  /_/  /_/\____/|___/_/\___/ ";

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
        }

        //Create Seating Choice ASCII Art
        public static void SeatingText()
        {
            string one =   @$"   _____            __   _            ";
            string two =   @$"  / ___/___  ____ _/ / _(_)___ ____ _";
            string three = $@"  \__ \/ _ \/ __ `/ __/ / __ \/ __ `/";
            string four =  @$" ___/ /  __/ /_/ / /_/ / / / / /_/ / ";
            string five =  @$"/____/\___/\__,_/\__/_/_/ /_/\__, /  ";
            string six =   @$"                            /____/   ";

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (one.Length / 2)) + "}", one));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (two.Length / 2)) + "}", two));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (three.Length / 2)) + "}", three));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (four.Length / 2)) + "}", four));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (five.Length / 2)) + "}", five));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (six.Length / 2)) + "}", six));
        }

    }
}
