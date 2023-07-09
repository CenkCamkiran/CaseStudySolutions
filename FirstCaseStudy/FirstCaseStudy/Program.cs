using System.Text;

Random r = new Random();
List<string> unshuffled = new List<string>() { "A", "C", "D", "E", "F", "G", "H", "K", "L", "M", "N", "P", "R", "T", "X", "Y", "Z", "2", "3", "4", "5", "7", "9" };
StringBuilder generatedText = new StringBuilder();

int size = 0;
int counter = 0;

//Fisher–Yates shuffle algoritmasi
while (counter < 1000) //1000 tane üretiliyor.
{
    for (int n = unshuffled.Count - 1; n > 0; --n)
    {
        int k = r.Next(n + 1);

        string temp = unshuffled[n];
        unshuffled[n] = unshuffled[k];
        unshuffled[k] = temp;

        generatedText.Append(unshuffled[n]);
        size++;

        if (size == 7) //Raandom string, 8 karakterli olacak
        {
            Console.WriteLine(generatedText.ToString());
            generatedText.Clear();
            counter++;
            size = 0;
            break;
        }

    }
}

