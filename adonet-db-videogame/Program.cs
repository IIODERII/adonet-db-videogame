namespace adonet_db_videogame
{
    public class TitoloVuotoException : Exception
    {
        public string Message => "Non puoi lasciare il titolo Vuoto";
    }
    public class DescrizioneVuotaException : Exception
    {
        public string Message => "Non puoi lasciare la descrizione vuota";
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Creiamo quindi una console app che all'avvio mostra un menu con la possibilità di :
            //> 1 inserire un nuovo videogioco
            //> 2 ricercare un videogioco per id
            //> 3 ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input
            //> 4 cancellare un videogioco
            //> 5 chiudere il programma

            int choise = 0;
            while(choise != 5)
            {

                Console.WriteLine("\nBenvenuto nel DataBase dei Videogiochi, scegli cosa vuoi fare: ");
                Console.WriteLine("\t1. Inserire un nuovo VideoGame");
                Console.WriteLine("\t2. Cercare un videogame per ID");
                Console.WriteLine("\t3. Cercare dei videogiochi per nome");
                Console.WriteLine("\t4. Eliminare un videogioco da id");
                Console.WriteLine("\t5. Chiudi il programma");

                Console.Write("\nInserire il numero relativo all'azione che si vuole utilizzare: ");
                choise = int.Parse(Console.ReadLine());
                if (choise == 1)
                {
                    var going = true;
                    string gameName = "";
                    string gameDescription = "";
                    DateTime gameDate = DateTime.Now;
                    int gameSoftId = 0;
                    while (going)
                    {
                        try
                        {
                            Console.Write("\nInserire Titolo del videogioco: ");
                            gameName = Console.ReadLine();
                            if (gameName == "")
                                throw new TitoloVuotoException();
                            Console.Write("Inserire la descrizione: ");
                            gameDescription = Console.ReadLine();
                            if (gameDescription == "")
                                throw new DescrizioneVuotaException();
                            Console.Write("Inserire la data di rilascio (gg/mm/aaaa): ");
                            gameDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Inserire l'ID della software house: ");
                            gameSoftId = int.Parse(Console.ReadLine());

                            going = false;
                        }
                        catch (TitoloVuotoException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (DescrizioneVuotaException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    VideogameManager.InserisciVideogame(gameName, gameDescription, gameSoftId, gameDate, DateTime.Now, DateTime.Now);

                    Console.WriteLine("Gioco aggiunto con successo");
                }
                else if (choise == 2)
                {
                    Console.Write("Inserire l'Id: ");
                    int id = int.Parse(Console.ReadLine());
                    Videogame gioco = VideogameManager.GetVideogameById(id);
                    Console.WriteLine($"Nome: {gioco.Name}");
                    Console.WriteLine($"Descrizione: {gioco.Overview}");
                    Console.WriteLine($"Data di rilascio: {gioco.Release_date}");
                }
                else if (choise == 3)
                {
                    Console.Write("Inserire la ricerca: ");
                    string ricerca = Console.ReadLine();
                    List<Videogame> videogames = VideogameManager.GetGamesBySearch(ricerca);

                    foreach (Videogame v in videogames)
                    {
                        Console.WriteLine($"Nome: {v.Name}");
                        Console.WriteLine($"Descrizione: {v.Overview}");
                        Console.WriteLine($"Data di rilascio: {v.Release_date}");
                    }
                }
                else if (choise == 4)
                {
                    Console.Write("Inserire l'Id del gioco da eliminare: ");
                    int id = int.Parse(Console.ReadLine());
                    VideogameManager.DeleteGame(id);
                    Console.WriteLine($"Videogame con id {id} eliminato correttamente");
                }
                else if(choise == 5)
                {
                    Console.WriteLine("Grazie per aver utilizzato il mio programma :)");
                }
                else
                {
                    Console.WriteLine("Input non valido.\n");
                }
            }
                       
        }
    }
}
