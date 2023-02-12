// double, для того, что бы в строке 6, в ситуации, когда у нас total < чем сумма процентов, не получать нули
Double total = 100;
var arrCases = new Double[] { 10,  5, 10, 7, 13, 15, 40};

// абсолютное значение числа подписок
var abs = arrCases.Select(x => x * (total/100)).ToArray();
Console.WriteLine(string.Join(", ", abs));

// словарь индекс : вес, где вес - это отношение ожидаемого числа действий к текущему
var lenDictionary = new Dictionary<int, Double>();

// конечная очередь
var res = new List<int>();

for (int i = 0; i < abs.Length; i++)
{
    // тк изначально у нас вес для всех = бесконечности (n / 0), выставим их абсолютное значение
    if (abs[i] >= 2)
    {
        lenDictionary[i] = abs[i];

    }
    // Костыль, что бы обрабатывать краевые значения
    else if ( Math.Round(abs[i]) == 2)
    {
        
        res.Add(i);
        res.Add(i);
        var weith = abs[i];
        lenDictionary[i] = weith/2 ;
    }
    // 2е условие, на случай, если у нас стоит 1% и 99% и 10 действий
    else if (Math.Round(abs[i]) == 1 || Math.Round(abs[i]) == 0)
    {
        res.Add(i);
        lenDictionary[i] = 0 ;

    }


    
}
Console.WriteLine(string.Join(", ", lenDictionary));


for (int i = 0; i < total; i++)
{
    var keyOfMaxValue = 
        lenDictionary.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
    res.Add(keyOfMaxValue);
    var oldWeith = lenDictionary[keyOfMaxValue];
    var count = res.Where(x => x == keyOfMaxValue).Count();
    var currentCountOfElements = count;
    var currentWaitValue = abs[keyOfMaxValue];
    lenDictionary[keyOfMaxValue] =  currentWaitValue/ currentCountOfElements;
    
    Console.WriteLine($"Добавил элемент: {keyOfMaxValue}, его вес был: {oldWeith}, его вес стал: {lenDictionary[keyOfMaxValue] }," +
                                        $" текущее количествво элементов: {currentCountOfElements}, ожидаемое: {currentWaitValue}");
}

for (int i =0; i < abs.Length; i++)
{
    var len = res.Where(x => x == i).Count();
    Console.WriteLine($"Элемент: {i}, Ожидалось: {abs[i]}, Получили: {len}");
}