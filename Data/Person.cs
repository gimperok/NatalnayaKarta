namespace NatalnayaKarta.Data
{
    /// <summary>
    /// Класс объекта участника "мероприятия"
    /// </summary>
    internal class Person
    {
        /// <summary>
        /// количество участников
        /// </summary>
        public static int personCount;
        /// <summary>
        /// Необходимая сумма на каждого участника
        /// </summary>
        public static double needPaid;
        private string name;
        private double money;

        public Person(string userName, double userMoney = 0)
        {
            Name = userName;
            Money = userMoney;
            personCount++;
        }
        /// <summary>
        /// Имя участника
        /// </summary>
        public string Name
        {
            set { name = value.Length > 10 ? value.Substring(0, 10) : value; }
            get { return name; }
        }
        /// <summary>
        /// Сколько участник оплатил до "мероприятия"
        /// </summary>
        public double Money
        {
            set { money = value > 999999999 ? 0 : value; }
            get { return money; }
        }
        /// <summary>
        /// Свойство с которого участник оплачивает долг коллегам
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// Платил ли участник до мероприятия
        /// </summary>
        public bool isPaid  => Money > 0;
        /// <summary>
        /// Позиция участника "мероприятия"
        /// </summary>
        public int Position { get; set; } = personCount + 1;

        /// <summary>
        /// Текущий участник оплачивает
        /// </summary>
        /// <param name="whoGetsPaid">Участник, который получает платеж</param>
        /// <param name="sumCurrentOperation">Сумма текущего платежа</param>
        /// <param name="totalPaidForCurrentPerson">Сколько всего оплатил участник</param>
        public void Pay(Person whoGetsPaid, double sumCurrentOperation, ref double totalPaidForCurrentPerson)
        {
            this.Balance -= sumCurrentOperation;
            whoGetsPaid.Balance += sumCurrentOperation;
            totalPaidForCurrentPerson += sumCurrentOperation;
        }
    }
}