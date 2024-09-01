using NatalnayaKarta.Data;

namespace NatalnayaKarta
{
    internal class Navigator
    {
        public Company company { get; set; }
        public Navigator()
        {
            company = new Company();
        }

        public async Task Begin()
        {
            foreach (var c in "Загружем натальную карту!")
            {
                await Task.Delay(30);
                Console.Write(c);
            }
            Console.WriteLine(Environment.NewLine);
        }

        public void CreatePersons()
        {
            Console.Write("Введите количество участников: ");
            var userInputPersonCount = Console.ReadLine();

            if (!string.IsNullOrEmpty(userInputPersonCount) && int.TryParse(userInputPersonCount, out int personCount))
            {
                Console.WriteLine("Если участник скидывался, впишите его имя и через 'пробел' сумму, иначе просто имя.");

                for (int i = 0; i < personCount; i++)
                {
                    Console.Write($"{Environment.NewLine}Имя участника №{i + 1}: ");
                    var userInputNameAndSumma = Console.ReadLine();

                    string userName = string.Empty;
                    double userMoney = 0;
                    if (!string.IsNullOrEmpty(userInputNameAndSumma))
                    {
                        var userNameAndSumma = userInputNameAndSumma.Split(' ');
                        userName = userNameAndSumma[0];
                        if (userNameAndSumma.Count() > 1)
                            double.TryParse(userNameAndSumma[1], out userMoney);
                    }

                    company.Colleagues.Add(new Person(userName, userMoney));
                }
            }
            else Environment.Exit(0);
        }

        public void CreateTableHead()
        {
            double summa = 0;
            company.Colleagues.ForEach(x => summa += x.Money);
            Person.needPaid = summa / Person.personCount;

            string line = ' ' + new String('_', 11 + Person.personCount * 11 + Person.personCount - 1);
            Console.WriteLine(line);
            Console.Write("|" + new String(' ', 10) + "|");//кубик вначале шапки

            foreach (Person person in company.Colleagues)   //рисуем шапку 
            {
                string cell = string.Empty;

                if (person.Name.Length < 11)
                    cell = person.Name.PadRight(11, ' ');
                Console.Write(cell + '|');
            }

            var line2 = String.Join("|", Enumerable.Repeat("___________", +Person.personCount + 1));
            Console.WriteLine(Environment.NewLine + '|' + line2.Substring(1, line2.Length - 1) + '|');
        }

        public void FillTable()
        {
            foreach (var person in company.Colleagues) //заполняем тамбличку
            {
                if (person.Money == 0) //если платил и сумма оплаты больше суммы на каждого, значит не скидывается
                    person.Balance = person.Money > Person.needPaid ? 0 : Person.needPaid;
                else if (person.Money < Person.needPaid)  //если сумма оплатым меньше суммы на кажждого, должен накинуть сколько не хватает
                    person.Balance = Person.needPaid - person.Money;

                List<string> res = new();//создаю секции каждой строки
                for (int i = 0; i < company.Colleagues.Count; i++)
                {
                    res.Add(string.Empty.PadRight(11, ' '));
                }
                double totalPaidForCurrentPerson = 0;

                foreach (var col in company.Colleagues)
                {
                    double currentOperation = 0;
                    if (person.Money >= Person.needPaid)
                        continue;
                    if (col.isPaid && person.Balance > 0 && totalPaidForCurrentPerson < col.Money - Person.needPaid)
                    {
                        if (person.Balance > 0 &&
                                person.Balance >= Person.needPaid &&
                                        col.Money - Person.needPaid - col.Balance >= Person.needPaid)
                        {
                            currentOperation = Person.needPaid;

                            person.Pay(col, currentOperation, ref totalPaidForCurrentPerson);
                        }
                        else if (person.Balance > 0 && totalPaidForCurrentPerson < Person.needPaid)
                        {
                            var collSum = col.Money - Person.needPaid - col.Balance; //сумма сбора коллеги без его части и того, что ему уже закинули
                            currentOperation = collSum > person.Balance ? person.Balance : collSum;

                            person.Pay(col, currentOperation, ref totalPaidForCurrentPerson);
                        }

                        res[col.Position - 1] = currentOperation == 0 ? string.Empty.PadRight(11, ' ') : $"{(int)Math.Ceiling(currentOperation)}-".PadRight(11, ' ');
                    }
                }

                res[person.Position - 1] = person.isPaid ? $"{person.Money}+".PadRight(11, ' ') : "*****X*****";

                var result = string.Join("|", res);
                Console.WriteLine('|' + person.Name.PadRight(10, ' ') + '|' + result + '|');
            }
            var line = String.Join("|", Enumerable.Repeat("___________", +Person.personCount + 1));
            Console.WriteLine('|' + line.Substring(1, line.Length - 1) + '|');
        }
    }
}