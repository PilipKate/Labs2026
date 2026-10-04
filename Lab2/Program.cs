namespace Lab2
{
    abstract class GameObject
    {
        public int Id;
        public string Name;
        public int X;
        public int Y;
        public GameObject(int Id, string Name, int X, int Y)
        {
            this.Id = Id;
            this.Name = Name;
            this.X = X;
            this.Y = Y;
        }

        public void newPosition(int x, int y)
        {
            X = x; Y = y;
        }
    }
    abstract class Unit : GameObject
    {
        public bool IsAlive;
        public float HP;
        public Unit(int Id, string Name, int X, int Y, float HP, bool IsAlive) : base(Id, Name, X, Y)
        {
            this.HP = HP;
            this.IsAlive = IsAlive;
        }
        public void receiveDamage(float damage)
        {
            HP -= damage;
            if (HP <= 0) IsAlive = false;
        }
    }
    interface IAttacker
    {
        void attack(Unit unit);
    }
    interface IMovable
    {
        void move(int X, int Y);
    }
    class Archer : Unit, IAttacker, IMovable
    {
        public Archer(int Id, string Name, int X, int Y, float HP, bool IsAlive) : base(Id, Name, X, Y, HP, IsAlive)
        {
        }
        public void attack(Unit unit)
        {
            unit.receiveDamage(50);
        }
        public void move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    abstract class Building : GameObject
    {
        bool IsBuilt;
        public Building(int Id, string Name, int X, int Y, bool IsBuilt) : base(Id, Name, X, Y)
        {
            this.IsBuilt = IsBuilt;
        }
    }
    class Fort : Building, IAttacker
    {
        public Fort(int Id, string Name, int X, int Y, bool IsBuilt) : base(Id, Name, X, Y, IsBuilt)
        {
        }
        public void attack(Unit unit)
        {
            unit.receiveDamage(50);
        }
    }
    class MobileHouse : Building, IMovable
    {
        public MobileHouse(int Id, string Name, int X, int Y, bool IsBuilt) : base(Id, Name, X, Y, IsBuilt)
        {
        }
        public void move(int x, int y)
        {
            newPosition(x, y);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Archer archer = new Archer(1, "Иван", 10, 20, 100, true);
            Console.WriteLine($"Имя: {archer.Name},  HP: {archer.HP},  Позиция: {archer.X}, {archer.Y}");
            archer.move(10, 14);
            Console.WriteLine("Позиция после движения: " + archer.X + ", " + archer.Y);
            Console.WriteLine();

            Archer archer2 = new Archer(2, "Владимир", 15, 25, 100, true);
            Console.WriteLine($"Имя: {archer2.Name},  HP: {archer2.HP},  Позиция: {archer2.X}, {archer2.Y}");
            archer.attack(archer2);
            Console.WriteLine("HP Владимира после атаки Ивана: " + archer2.HP);
            Console.WriteLine();

            Fort fort = new Fort(3, "Крепость", 80, 44, true);
            Console.WriteLine($"Название форта: {fort.Name},  Позиция: {fort.X}, {fort.Y}");
            fort.Equals(archer);
            Console.WriteLine("HP Ивана после атаки крепостью: " + archer2.HP);
            Console.WriteLine();

            MobileHouse house = new MobileHouse(4, "Дом", 29, 48, true);
            Console.WriteLine($"Название дома: {house.Name},  Позиция: {house.X}, {house.Y}");
            house.move(2, 3);
            Console.WriteLine($"Позиция дома после движения: {house.X}, {house.Y}");
            Console.WriteLine();
        }
    }
}
