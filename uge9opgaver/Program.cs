using System;
using System.Collections.Generic;

namespace uge9opgaver
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            enEllerAndenMetode( CPHPersonManager.Instance());
        }

		public static void enEllerAndenMetode(IPersonManager pm)
		{
			Person p1 = pm.createPerson("Hans", "hans.jensen@gmail.com", 43109812);
			Person p2 = pm.createPerson("Gurli", "gurli.ibsen@gmail.com", 72651890);
			Person p3 = pm.createPerson("Ali", "Ali.Hansen@gmail.com", 21897921);

			Person p = pm.findFromPhone(43109812);
            Console.WriteLine("Fandt: " + p.Name);
            Console.ReadLine();
        }
    }

    interface IPersonManager
    {
        Person createPerson(string name, string email, int phone);
        Person findFromPhone(int v);
    }

    class PersonManager : IPersonManager{
        protected static PersonManager instance;
        public static PersonManager Instance(){
            if (instance == null) instance = new PersonManager();
            return instance;
        }
        protected PersonManager(){
            idCounter = 1;
            pl = new List<Person>();
        }

        private int idCounter;
        private IList<Person> pl;

        public virtual Person createPerson(string name, string email, int phone)
        {
            Person newPerson = new Person(name, email, phone);
            newPerson.ID = idCounter++;
            pl.Add(newPerson);
            return newPerson;
        }

        public Person findFromPhone(int phone){
            foreach (Person p in pl){
                if (p.Phone == phone) return p;
            }
            return null;
        }
    }
    class CPHPersonManager : PersonManager{
		public new static PersonManager Instance()
		{
			if (instance == null) instance = new CPHPersonManager();
			return instance;
		}
        public override Person createPerson(string name, string email, int phone){
            if (!email.Contains("@cphbusiness.dk"))
                throw new ArgumentException();
            return base.createPerson(name, email, phone);
        }
    }

    class Person {
		public int ID { get; set; }
		public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public Person (string name, string email, int phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}
