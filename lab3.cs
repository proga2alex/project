using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace C__OOP
{
    internal class Program
    {
        public abstract class Entity
        {
            public int Id { get; }
            public Entity(int id) => Id = id;
        }

        public class Bantic : Entity
        {
            public Bantic(int id) : base(id) { } 
            public uint price { get; set; }
            public double size { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string color { get; set; }
            public string brend { get; set; }
        }


        public interface IRepository<T>
        {
            T GetById(int id);
            List<T> GetAll();
            void Add(T entity);
          //  void Update(T entity);
            void Remove(T entity);
        }

        class Repository<T> : IRepository<T> where T : Entity
        {
            private List<T> items = new List<T>();

            public void Add(T item) => items.Add(item);
            public void Remove(T item) => items.Remove(item);
            public T GetById(int Id) => items.FirstOrDefault(x => x.Id == Id);
            public List<T> GetAll() => items.ToList();
            public List<T> GetByCondition(Func<T, bool> condition) => items.Where(condition).ToList();
            public List<T> GetSorted(Func<T, object> keySelector) => items.OrderBy(keySelector).ToList();
        }

        public class user
        {
            public uint ID;
            public string role;
            public string email;
            public string name;
            public string phone_num;
            public string password;

            public virtual void i_am()
            {
                Console.WriteLine("я юзер");
            }
        }

        public class Admin : user
        {
            public int admin_level;

            public override void i_am()
            {
                Console.WriteLine("я адмін");
            }
        }

        static void Main(string[] args)
        {
            Repository<Bantic> bantRep = new Repository<Bantic>();
            Bantic newBantic = new Bantic(1) { name = "бантік червоний", price = 300, color = "червоний" };
            bantRep.Add(newBantic);
            bantRep.Add(new Bantic(2) { name = "бантік синій", price = 1488, color = "синій" });
            bantRep.Add(new Bantic(3) { name = "рожевий бантік", price = 228, color = "рожевий" });

            Console.WriteLine("\n список бантиків");
            foreach(var BAN in bantRep.GetAll())
            {
                Console.WriteLine($"Id = {BAN.Id}, Name = {BAN.name}, Color= {BAN.color}, Price = {BAN.price} ");
            }
            Console.WriteLine("\n почаиок назви");
            var filtered = bantRep.GetByCondition(p => p.name.StartsWith("б"));
            foreach (var BAN in filtered)
            {
                Console.WriteLine($"Id = {BAN.Id}, Name = {BAN.name}, Color= {BAN.color}, Price = {BAN.price} ");
            }
            bantRep.Add(new Bantic(4) { name = "чорний бантік", price = 1000, color = "чорний" });

            Console.WriteLine("\n список бантиків");
            foreach (var BAN in bantRep.GetAll())
            {
                Console.WriteLine($"Id = {BAN.Id}, Name = {BAN.name}, Color= {BAN.color}, Price = {BAN.price} ");
            }
            bantRep.Remove(bantRep.GetById(4));

            Console.WriteLine("\n список бантиків");
            foreach (var BAN in bantRep.GetAll())
            {
                Console.WriteLine($"Id = {BAN.Id}, Name = {BAN.name}, Color= {BAN.color}, Price = {BAN.price} ");
            }
            Console.WriteLine("\n сортування");
            var sorted2 = bantRep.GetSorted(p => p.name);
            foreach(var BAN in sorted2)
            {
                Console.WriteLine($"Id = {BAN.Id}, Name = {BAN.name}, Color= {BAN.color}, Price = {BAN.price} ");
            }



        }

        
    }
}
