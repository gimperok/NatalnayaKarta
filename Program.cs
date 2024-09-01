using NatalnayaKarta;

Navigator navigator = new Navigator();

await navigator.Begin();
navigator.CreatePersons();
navigator.CreateTableHead();
navigator.FillTable();
Console.ReadLine();