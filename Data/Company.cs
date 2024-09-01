namespace NatalnayaKarta.Data
{
    /// <summary>
    /// Класс со списком участников "мероприятия"
    /// </summary>
    internal class Company
    {
        /// <summary>
        /// Список участников "мероприятия"
        /// </summary>
        public List<Person> Colleagues { get; set; } = new();
    }
}
