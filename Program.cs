// 37345-12345-12345 valid

// 4, 5, 37 => valid

// 12:345-12345-12345 => not valid

string[] tickets = {
    "52345-12345-12345",
    "370345-12345-12345",
    "42345-12345-12345",
    "345-12345-12345",
    "92345-12345-12345",
    "4245:12345:12345",
    "37345-12345-12345",
    "12345:12345-12345",
};

foreach (var item in tickets)
{
    var value = item.Split("-");

    if (value.Length != 3)
    {
        Console.WriteLine("no");
        continue;
    }

    for (int i = 0; i < value.Length; i++)
    {
        var word = value[i];

        if (word.Length != 5)
        {
            Console.WriteLine("no");
            break;
        }

        if (i == 0)
        {
            if(word[0] == '3')
            {
                if (word[1] != '7')
                {
                    Console.WriteLine("no");
                    break;
                }
            } else
            {
                if (word[0] != '4' && word[0] != '5')
                {
                    Console.WriteLine("no");
                    break;
                }
            }
        }

        if (i == value.Length - 1)
        {
            Console.WriteLine("yes");
        }
    }
}