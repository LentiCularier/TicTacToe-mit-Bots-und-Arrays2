using System;

namespace TicTacToeMitBot
{
    class Program
    {
        static bool debuggingAn = false; //wenn auf true, aktiviert dies die Debugging Nachrichten in der Console, damit man sehen kann, was der Bot sich "gedacht" hat
                                 //alternativ lässt sich dies jedoch auch durch die Eingabe von "debug" bei der ersten Frage des Bots nach dem Spielstart aktivieren 

        public static string[,] felder = {
                                       { " ", " ", " " },
                                       { " ", " ", " " },
                                       { " ", " ", " " }
                                  };

        static string spielerSymbol;

        static string botSymbol;

        static bool siegLiegtVor;

        static int spielZugAnzahl = 0;

        static void Main(string[] args)
        {
            SpielStart();

            while (true)
            {
                EndeOhneSiegUeberpruefung();
                spielZugAnzahl++;
                ZugDesSpielersAufforderung();
                GeneriereSpielfeld();
                SiegueberpruefungSpieler();

                EndeOhneSiegUeberpruefung();
                spielZugAnzahl++;
                ZugDesBots();
                UeberlegeSimulation();
                GeneriereSpielfeld();
            }
        }

        static void SpielStart()
        {
            Console.WriteLine("╔═══════════════════════════════════════TIC-TAC-TOE═════════════════════════════════════════╗");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║ Hallo! Ich bin Mr. Kong-Long-Schlong und der beste Tic-Tac-Toe spieler auf dieser Erde.   ║");
            Console.WriteLine("║ Mich besiegt niemand!                                                                     ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║ Du möchtest es also aber trotzdem wagen gegen mich anzutreten? [ja/nein]                  ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            JaNein();
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ OK! Gewagt, gewagt mein junger Padawan!                                                   ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║ Die Spielfelder werden folgende Nummerierung tragen:                                      ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║                                 ╔═════╦═════╦═════╗                                       ║");
            Console.WriteLine("║                                 ║  1  ║  2  ║  3  ║                                       ║");
            Console.WriteLine("║                                 ╠═════╬═════╬═════╣                                       ║");
            Console.WriteLine("║                                 ║  4  ║  5  ║  6  ║                                       ║");
            Console.WriteLine("║                                 ╠═════╬═════╬═════╣                                       ║");
            Console.WriteLine("║                                 ║  7  ║  8  ║  9  ║                                       ║");
            Console.WriteLine("║                                 ╚═════╩═════╩═════╝                                       ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║ Möchtest du anfangen oder soll ich beginnen? [ich/du]                                     ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            DuOderIch();

        }//Konsolenausgabe bei Spielstart und Fragen an den Spieler, ob er spielen möchte und wer anfängt

        static void EndeOhneSiegUeberpruefung()
        {
            if (spielZugAnzahl == 9)
            {
                EndeOhneSieg();
            }
        }//prüft, ob nicht schon der 9. Spielzug vorliegt und somit alle Felder bereits voll sind 

        static void EndeOhneSieg()
        {
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║                        So wie es aussieht, sind alle Felder voll!                         ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║        Meine langjährige Erfahrung sagt mir, dass dies ein Unentschieden bedeutet.        ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║                            Ich sag doch - mich besiegt keiner                             ║");
            Console.WriteLine("║                                     HAHHAHAHAHA                                           ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                   [Drücke die Eingabetaste, um das Spiel zu beenden]                      ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════╝");
            System.Console.ReadLine();
            Environment.Exit(0);
        }//Konsolenausgabe für ein Ende des Spiels, wenn alle Felder voll sind

        static void PruefeSiegmoeglichkeit()
        {
            siegLiegtVor = false; //setzt Variable siegLiegtVor vor dem Ausführen der Überprüfung zurück

            //prüft ob entlang der ZEILEN im Spielfeld alle 3 eingesetzten Zeichen gleich sind:
            for (int z = 0; z <= 2; z++)
            {
                if (felder[z, 0] == felder[z, 1] && felder[z, 1] == felder[z, 2] && felder[z, 1] != " ")
                {
                    DebugNachricht("DEBUG: Die Funktion PruefeSiegmoeglichkeit hat in Zeile " + z + " drei gleiche Zeichen gefunden.");
                    siegLiegtVor = true;
                }
            }

            //prüft ob entlang der SPALTEN im Spielfeld alle 3 eingesetzten Zeichen gleich sind:
            for (int s = 0; s <= 2; s++)
            {
                if (felder[0, s] == felder[1, s] && felder[1, s] == felder[2, s] && felder[0, s] != " ")
                {
                    DebugNachricht("DEBUG: Die Funktion PruefeSiegmoeglichkeit hat in Spalte " + s + " drei gleiche Zeichen gefunden.");
                    siegLiegtVor = true;
                }
            }

            //prüft ob entlang der HAUPTDIAGONALEN im Spielfeld alle 3 eingesetzten Zeichen gleich sind:
            if (felder[0, 0] == felder[1, 1] && felder[1, 1] == felder[2, 2] && felder[0, 0] != " ")
            {
                DebugNachricht("DEBUG: Die Funktion PruefeSiegmoeglichkeit hat in der Hauptdiagonalen drei gleiche Zeichen gefunden.");
                siegLiegtVor = true;
            }


            //prüft ob entlang der NEBENDIAGONALEN im Spielfeld alle 3 eingesetzten Zeichen gleich sind
            if (felder[0, 2] == felder[1, 1] && felder[1, 1] == felder[2, 0] && felder[0, 2] != " ")
            {
                DebugNachricht("DEBUG: Die Funktion PruefeSiegmoeglichkeit hat in der Nebendiagonalen drei gleiche Zeichen gefunden.");
                siegLiegtVor = true;
            }

        }//prüft ob aktuell ein Sieg vorliegt

        static void SiegueberpruefungSpieler()
        {
            PruefeSiegmoeglichkeit();

            if (siegLiegtVor == true)
            {
                SiegDesSpielers();
            }
        }//prüft durch Ausührung von PruefeSiemöglichkeit, ob der Spieler gewonnen hat, wenn ja wird SiegDesSpielers ausgeführt

        static void JaNein()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "ja":
                    break;
                case "nein":
                    Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine("║ Schwach von dir - dann halt nicht!                                                        ║");
                    Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════╝");
                    Environment.Exit(0);
                    break;
                case "debug":
                    debuggingAn = true;
                    DebugNachricht("DEBUG: Die Debugnachrichten wurden erfolgreich aktiviert.");
                    break;
                default:
                    Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine("║ Was gibst du da von dir? Antworte gefälligst venünftig!                                   ║");
                    Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
                    JaNein();
                    break;

            }
        }//fragt den Spieler beim Spielstart ob er wirklich gegen den Bot antreten möchte
                               //Außerdem ist hier auch eine versteckte Funktion zum Aktivieren der Debug-Nachrichten über die Konsole implementiert

        static void DuOderIch()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "ich":
                    botSymbol = "O";
                    spielerSymbol = "X";
                    break;
                case "du":
                    spielerSymbol = "O";
                    botSymbol = "X";
                    Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine("║ Danke! Sehr großzügig von dir!                                                            ║");
                    Console.Write("║ So lasse mich den ersten Zug ausführen ");
                    System.Threading.Thread.Sleep(250);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(250);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(250);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(250);
                    Console.Write("hmm");
                    Console.Write(".");
                    System.Threading.Thread.Sleep(250);
                    Console.Write(".");
                    System.Threading.Thread.Sleep(500);
                    Console.Write(".");
                    Console.WriteLine("                                          ║");
                    felder[1, 1] = "X";
                    spielZugAnzahl++;
                    GeneriereSpielfeld();
                    break;
                default:
                    Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine("║ Wie bitte? Verstehe ich nicht...                                                          ║");
                    Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
                    DuOderIch();
                    break;

            }
        }//fragt den Spieler wer anfangen soll und wählt dem entsprechend BotSymbol und SpielerSymbol.
                                  //Fängt Bot an wird außerdem hier ein X in Feld 5 gesetzt und es ist auch eine Wartesimulation für diesen Zug enthalten

        static void GeneriereSpielfeld()
        {
            Console.WriteLine("╠═════════════════════════════════════════╣" + spielZugAnzahl + "╠═══════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║                                 ╔═════╦═════╦═════╗                                       ║");
            Console.WriteLine("║                                 ║  " + felder[0, 0] + "  ║  " + felder[0, 1] + "  ║  " + felder[0, 2] + "  ║                                       ║");
            Console.WriteLine("║                                 ╠═════╬═════╬═════╣                                       ║");
            Console.WriteLine("║                                 ║  " + felder[1, 0] + "  ║  " + felder[1, 1] + "  ║  " + felder[1, 2] + "  ║                                       ║");
            Console.WriteLine("║                                 ╠═════╬═════╬═════╣                                       ║");
            Console.WriteLine("║                                 ║  " + felder[2, 0] + "  ║  " + felder[2, 1] + "  ║  " + felder[2, 2] + "  ║                                       ║");
            Console.WriteLine("║                                 ╚═════╩═════╩═════╝                                       ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                                           ║");
        }//gibt das aktuelle Spielfeld in der Konsole aus

        static void FeldIstVoll()//Konsolenausgabe wenn der Spieler sein Zeichen in ein volles Feld setzen möchte
        {
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║Bitte was? Das Feld ist doch schon voll!                                                   ║");
            Console.WriteLine("║Wähle ein anderes Feld                                                                     ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            ZugDesSpielers();
        }

        static void ZugDesSpielers() //liest die Einbabe der Spielers in die Konsole aus, wenn dieser dran ist
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    if (felder[0, 0] == " ")
                    {
                        felder[0, 0] = spielerSymbol;
                        break;
                    }
                    else
                    {
                        FeldIstVoll();
                        break;
                    }

                case "2":
                    if (felder[0, 1] == " ")
                    {
                        felder[0, 1] = spielerSymbol;
                        break;
                    }
                    else
                    {
                        FeldIstVoll();
                        break;
                    }

                case "3":
                    if (felder[0, 2] == " ")
                    {
                        felder[0, 2] = spielerSymbol;
                        break;
                    }
                    else
                    {
                        FeldIstVoll();
                        break;
                    }

                case "4":
                    if (felder[1, 0] == " ")
                    {
                        felder[1, 0] = spielerSymbol;
                        break;
                    }
                    else
                    {
                        FeldIstVoll();
                        break;
                    }

                case "5":
                    if (felder[1, 1] == " ")
                    {
                        felder[1, 1] = spielerSymbol;
                        break;
                    }
                    else
                    {
                        FeldIstVoll();
                        break;
                    }

                case "6":
                    if (felder[1, 2] == " ")
                    {
                        felder[1, 2] = spielerSymbol;
                        break;
                    }
                    else
                    {
                        FeldIstVoll();
                        break;
                    }

                case "7":
                    if (felder[2, 0] == " ")
                    {
                        felder[2, 0] = spielerSymbol;
                        break;
                    }
                    else
                    {
                        FeldIstVoll();
                        break;
                    }

                case "8":
                    if (felder[2, 1] == " ")
                    {
                        felder[2, 1] = spielerSymbol;
                        break;
                    }
                    else
                    {
                        FeldIstVoll();
                        break;
                    }

                case "9":
                    if (felder[2, 2] == " ")
                    {
                        felder[2, 2] = spielerSymbol;
                        break;
                    }
                    else
                    {
                        FeldIstVoll();
                        break;
                    }



                default:


                    Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine("║Hmm - Das war aber keine Zahl von 1-9!                                                     ║");
                    Console.WriteLine("║Versuche es noch einmal und lerne schreiben!                                               ║");
                    Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
                    ZugDesSpielers();
                    break;
            }
        }

        static void SiegDesSpielers() //Konsolenausgabe, wenn Spieler gewinnt
        {
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                           OHA                                             ║");
            Console.WriteLine("║                               Ich kann es nicht fassen...                                 ║");
            Console.WriteLine("║                    Du hast den besten Tic-Tac-Toe Spieler der Welt besiegt!               ║");
            Console.WriteLine("║                     Ab jetzt bist du es wohl, der diesen Titel tragen wird!               ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║                                  HERZLICHEN GLÜCKWUNSCH!                                  ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║                     Ich werde mich nun zurückziehen und noch etwas üben.                  ║");
            Console.WriteLine("║                           Auf Wiedersehen, werter Spielpartner!                           ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                   [Drücke die Eingabetaste, um das Spiel zu beenden]                      ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════╝");
            System.Console.ReadLine();
            Environment.Exit(0);

        }

        static void SiegDesBots()
        {
            GeneriereSpielfeld();
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║                             Tja - da habe ich wohl gewonnen!                              ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("║                           Ich sag doch - mich besiegt keiner!                             ║");
            Console.WriteLine("║                                     HAHHAHAHAHA                                           ║");
            Console.WriteLine("║                                                                                           ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                   [Drücke die Eingabetaste, um das Spiel zu beenden]                      ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════╝");
            System.Console.ReadLine();
            Environment.Exit(0);
        } //Konsolenausgabe wenn Bot gewinnt

        static void ZugDesSpielersAufforderung() //fodert den Spieler auf ein Feld für sein Zeichen zu setzen
        {
            Console.WriteLine("║ Du bist dran! Gebe eine Zahl von 1-9 ein, um dein " + spielerSymbol + " auf das entsprechende Feld zu setzen! ║");
            Console.WriteLine("║                          Drücke anschließend die Eingabetaste.                            ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════════════════════════════════════════════╣");
            ZugDesSpielers();
        }

        static void ZugDesBots()
        {
            //1. Hier wird geschaut, ob der Bot im aktuellen Spielzug gewinnen könnte:
            //wenn ja, wird ein Sieg des Bots ausgerufen und das Spiel ist zu Ende

            for (int zeile = 0; zeile <= 2; zeile++)
            {
                for (int spalte = 0; spalte <= 2; spalte++)
                {
                    if (felder[spalte, zeile] == " ")
                    {
                        felder[spalte, zeile] = botSymbol;
                        PruefeSiegmoeglichkeit();
                        if (siegLiegtVor == true)
                        {
                            DebugNachricht("DEBUG: Bot hat Siegmöglichkeit für sich in Feld [" + zeile + "," + spalte + "] erkannt.");
                            SiegDesBots();
                        }
                        else
                        {
                            felder[spalte, zeile] = " ";
                        }
                    }
                }

            }

            DebugNachricht("DEBUG: Bot konnte im aktuellen Zug keine Siegmöglichkeit für sich finden" +
                "\n       Es wird nun geschaut, ob ein Sieg des Gegenspielers im nächsten Zug möglich ist, um diesen zu verhindern." +
                "\n       Hierfür wird nun testweise in jedes Feld das Gegnersymbol gesetzt" +
                "\n       und anschließend geprüft ob eine Siegmöglichkeit vorliegt.");

            //2. Wenn 1. nicht erfolgreich wird hier geprüft, ob der Gegenspieler im nächsten Zug gewinnen könnte:
            //Wenn ja wird in das entsprechende Feld vom Bot das BotSymbol gesetzt, um den Sieg des Nutzers im nächsten Zug verhindern

            DebugNachricht("DEBUG: Es wird nun testweise in jedes Feld das Gegnersymbol gesetzt\n" +
                "       und anschließend geprüft ob eine Siegmöglichkeit vorliegt.");

            for (int zeile = 0; zeile <= 2; zeile++)
            {
                for (int spalte = 0; spalte <= 2; spalte++)
                {
                    if (felder[spalte, zeile] == " ")
                    {
                        felder[spalte, zeile] = spielerSymbol;
                        PruefeSiegmoeglichkeit();
                        if (siegLiegtVor == true)
                        {
                            DebugNachricht("DEBUG: Bot hat Siegmöglichkeit des Gegenspielers im nächsten Zug gefunden \n" +
                         "       Deshalb setzt dieser nun in Feld [" + zeile + "," + spalte + "] sein Zeichen, um den Sieg zu verhindern.");

                            felder[spalte, zeile] = botSymbol;
                            return;
                        }
                        else
                        {
                            felder[spalte, zeile] = " ";
                        }
                    }
                }

            }

            // 3. Wenn 1. und 2. nicht erfolgreich, dann wird hier nun das Botsymbol in ein zufälliges freies Feld gesetzt:


            DebugNachricht("DEBUG: Bot hat weder eine Siegmöglichkeit für sich im aktuellen Zug, noch eine für den\n" +
                        "       Gegenspieler im nächsten Zug gefunden. Deshalb wird nun ein Zeichen in ein zufälliges freies Feld gesetzt");
            ZeichenInZufaelligesFreiesFeld();

        }

        static void ZeichenInZufaelligesFreiesFeld()
        {
            Random myObject1 = new Random();
            int ranNum1 = myObject1.Next(0, 3);
            Random myObject2 = new Random();
            int ranNum2 = myObject2.Next(0, 3);

            DebugNachricht("DEBUG: Es wurde Feld [" + ranNum1 + "," + ranNum2 + "]gewählt. Nun wird geprüft ob dieses leer ist...");

            if (felder[ranNum1, ranNum2] == " ")
            {
                felder[ranNum1, ranNum2] = botSymbol;
                DebugNachricht("DEBUG: Das gewählte Feld ist frei und das BotSymbol wurde dort hinein gesetzt.");
                return;
            }
            else
            {
                DebugNachricht("DEBUG: Das gewählte Feld scheint voll zu sein. Es wird ein neues Feld ausprobiert:");
                ZeichenInZufaelligesFreiesFeld();
            }


        } //setzt das Zeichen des Bots in zufälliges freies Feld, wenn oben aufgführte Methoden erfolglos sind

        static void UeberlegeSimulation()//sorgt für die Verzoegerung, wenn der Bot dran ist
        {
            Console.Write("║ Jetzt bin ich aber wieder dran");
            System.Threading.Thread.Sleep(250);
            Console.Write(".");
            System.Threading.Thread.Sleep(250);
            Console.Write(".");
            System.Threading.Thread.Sleep(250);
            Console.Write(".");
            System.Threading.Thread.Sleep(250);
            Console.Write("hmm");
            Console.Write(".");
            System.Threading.Thread.Sleep(250);
            Console.Write(".");
            System.Threading.Thread.Sleep(500);
            Console.Write(".");
            Console.WriteLine("                                                   ║");
            Console.WriteLine("║                                                                                           ║");
        }

        static void DebugNachricht(string nachricht)
        {
            if (debuggingAn == true)
            {
                Console.WriteLine(nachricht);
            }

        }

    }
}


