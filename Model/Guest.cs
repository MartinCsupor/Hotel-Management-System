namespace Hotel_Management_System.Model
{
    public class Guest
    {
        protected int id;
        protected string name;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        public Guest(int id, string name)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(name))
                throw new System.ArgumentException("A vendég adatai érvénytelenek.");

            Id = id;
            Name = name.Trim();
        }

        public override string ToString()
        {
            return string.Format("{0}. {1}", Id, Name);
        }
    }
}
