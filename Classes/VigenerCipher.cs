using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFavoriteEncryption.Classes
{
  public class VigenerCipher
  {

    private static string alphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

    private static int N = alphabet.Length;

    public static bool CheckingKey(string keyword)
    {
      List<char> keywordChar = keyword.ToList();
      return keywordChar.All(u => alphabet.Contains(Char.ToLower(u)) == true);
    }

    public static string Encode(string input, string keyword)
    {
      string result = "";
      keyword = keyword.ToLower();
      int keyword_index = 0;

      foreach (char symbol in input)
      {
        if (alphabet.Contains(symbol))
        {
          int c = (alphabet.IndexOf(symbol) +
          alphabet.IndexOf(keyword[keyword_index])) % N;

          if (Char.IsUpper(symbol))
            result += Char.ToUpper(alphabet[c]);
          else
            result += alphabet[c];

          keyword_index++;
        }
        else
        {
          result += symbol;
        }

        if ((keyword_index) == keyword.Length)
          keyword_index = 0;
      }

      return result;
    }

    public static string Decode(string input, string keyword)
    {
      string result = "";
      if (keyword != null && keyword.Length != 0)
        keyword = keyword.ToLower();

      int keyword_index = 0;

      foreach (char symbol in input)
      {
        int p = -1;

        if (alphabet.Contains(symbol))
        {
          p = (alphabet.IndexOf(symbol) + N -
            alphabet.IndexOf(keyword[keyword_index])) % N;

          if (Char.IsUpper(symbol))
            result += Char.ToUpper(alphabet[p]);
          else
            result += alphabet[p];

          keyword_index++;
        }
        else
        {
          result += symbol;
        }

        if ((keyword_index) == keyword.Length)
          keyword_index = 0;
      }

      return result;
    }
  }
}
