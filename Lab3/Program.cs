using System.Runtime.InteropServices;

namespace Lab3
{
    // Singletone
    public class StarOfSolarSystem
    {
        private static StarOfSolarSystem star;
        private string starName;
        private int size;
        private StarOfSolarSystem()
        {
            this.starName = "Sun";
            this.size = 53468;
        }

        public static StarOfSolarSystem getStar()
        {
            if (star == null)
            {
                star = new StarOfSolarSystem();
            }
            return star;
        }
        public void getStarName()
        {
            Console.WriteLine(this.starName);
        }
    }



    // Factory method
    public interface ITaxi
    {
        void Car(string model);
    }
    public class BusinessTaxi : ITaxi
    {
        public void Car(string model)
        {
            Console.WriteLine("Ожидайте такси бизнесс класса: " + model);
        }
    }
    public class EconomyTaxi : ITaxi
    {
        public void Car(string model)
        {
            Console.WriteLine("Ожидайте такси эконом класса: " + model);
        }
    }
    public abstract class TaxiFactory
    {
        public abstract ITaxi CreateTaxi();
        public void TaxiMessage(string model)
        {
            ITaxi taxi = CreateTaxi();
            taxi.Car(model);
        }
    }
    public class BusinessTaxiFactory : TaxiFactory
    {
        public override ITaxi CreateTaxi()
        {
            return new BusinessTaxi();
        }
    }
    public class EconomyTaxiFactory : TaxiFactory
    {
        public override ITaxi CreateTaxi()
        {
            return new EconomyTaxi();
        }
    }



    //Abstract factory
    public interface IBurger
    {
        void Cook();
    }
    public class McDonaldsBurger : IBurger
    {
        public void Cook()
        {
            Console.WriteLine("Бургер из McDonald's");
        }
    }
    public class KfcBurger : IBurger
    {
        public void Cook()
        {
            Console.WriteLine("Бургер из Kfc");
        }
    }
    public interface INuggets
    {
        void Cook();
    }
    public class McDonaldsNuggets : INuggets
    {
        public void Cook()
        {
            Console.WriteLine("Наггетсы из McDonald's");
        }
    }

    public class KfcNuggets : INuggets
    {
        public void Cook()
        {
            Console.WriteLine("Наггетсы из Kfc");
        }
    }
    public interface IFactory
    {
        INuggets CookNuggets();
        IBurger CookBurger();
    }
    public class McDonaldsFactory : IFactory
    {
        public INuggets CookNuggets()
        {
            return new McDonaldsNuggets();
        }

        public IBurger CookBurger()
        {
            return new McDonaldsBurger();
        }
    }
    public class KfcFactory : IFactory
    {
        public INuggets CookNuggets()
        {
            return new KfcNuggets();
        }

        public IBurger CookBurger()
        {
            return new KfcBurger();
        }
    }
    public class Application
    {
        private readonly INuggets _nuggets;
        private readonly IBurger _burger;

        public Application(IFactory factory)
        {
            _nuggets = factory.CookNuggets();
            _burger = factory.CookBurger();
        }

        public void Cook()
        {
            _nuggets.Cook();
            _burger.Cook();
        }
    }




    //Builder
    public class Appartments
    {
        public int? rooms { get; set; }
        public int? price { get; set; }

        public override string ToString()
        {
            return $"Квартира: Количество комнат: {rooms};  Цена: {price};";
        }
    }
    public interface IBuilder
    {
        void BuildRooms();
        void BuildPrice();
        Appartments GetResult();
    }
    public class BigAppartmentBuilder : IBuilder
    {
        private Appartments appart;

        public BigAppartmentBuilder()
        {
            appart = new Appartments();
        }

        public void BuildRooms()
        {
            appart.rooms = 10;
        }

        public void BuildPrice()
        {
            appart.price = 100000000;
        }

        public Appartments GetResult()
        {
            return appart;
        }
    }
    public class AppartmentDirector
    {
        private IBuilder builder;

        public AppartmentDirector(IBuilder builder)
        {
            this.builder = builder;
        }

        public void CreateAppartments()
        {
            builder.BuildRooms();
            builder.BuildPrice();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Singletone
            StarOfSolarSystem star = StarOfSolarSystem.getStar();
            star.getStarName();
            Console.WriteLine();


            //Factory method
            string taxiChoice = "economy";
            TaxiFactory factory = taxiChoice switch
            {
                "business" => new BusinessTaxiFactory(),
                "economy" => new EconomyTaxiFactory(),
            };
            factory.TaxiMessage("Мерседес");
            Console.WriteLine();


            //Abstract factory
            string foodCompany1 = "McDonalds";
            IFactory restourant1 = foodCompany1 switch
            {
                "McDonalds" => new McDonaldsFactory(),
                "Kfc" => new KfcFactory(),
            };
            Application app1 = new Application(restourant1);
            app1.Cook();
            Console.WriteLine();

            string foodCompany2 = "Kfc";
            IFactory restourant2 = foodCompany2 switch
            {
                "McDonalds" => new McDonaldsFactory(),
                "Kfc" => new KfcFactory(),
            };
            Application app2 = new Application(restourant2);
            app2.Cook();
            Console.WriteLine();


            //Builder
            IBuilder builder = new BigAppartmentBuilder();
            AppartmentDirector director = new AppartmentDirector(builder);

            director.CreateAppartments();
            Appartments appart = builder.GetResult();

            Console.WriteLine(appart);

        }
    }
}
