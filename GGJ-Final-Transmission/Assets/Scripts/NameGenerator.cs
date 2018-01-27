﻿using UnityEngine;
using System.Collections;

public static class NameGenerator
{

    private static string[] namesFirst = new string[]
{
"Brad",
"Shane",
"Bobby",
"Jeremy",
"Jessica",
"Michelle",
"Ashley",
"Brittany",
"Frederic",
"Guillaume",
"Jacques",
"Yves",
"Camille",
"Isabelle",
"Madeleine",
"Renee",
"Juan Carlos",
"Ricardo",
"Santiago",
"Jorge",
"Anamaria",
"Guadalupe",
"Beatriz",
"Rosa",
"Klaus",
"Ernst",
"Heinrich",
"Markus",
"Ursula",
"Gertrud",
"Jana",
"Claudia",
"Honghui",
"Guang",
"Weisheng",
"Feng",
"Mei",
"Yin",
"Xiuying",
"Xiaolian",
"Daichi",
"Masahiro",
"Yutaka",
"Kenji",
"Aiko",
"Kazumi",
"Sakura",
"Natsuki",
"Min Jae",
"Min Ho",
"Minsu",
"Moon",
"Ji Hye",
"Hye Jin",
"Soon Bok",
"Sun Hi",
"Amit",
"Nikhil",
"Rohit",
"Suresh",
"Priya",
"Kalia",
"Lina",
"Shivani",
"Ahmed",
"Ali",
"Abdullah",
"Hassan",
"Safiya",
"Aya",
"Fatima",
"Nasreem",
"Ayokunle",
"Chike",
"Kwame",
"Nkosana",
"Asha",
"Caimile",
"Zalika",
"Kesia",
"Fabricio",
"Maurizio",
"Lorenzo",
"Antonio",
"Donatella",
"Ariana",
"Valentina",
"Alessia",
"Vladimir",
"Nikolai",
"Konstantin",
"Stanislav",
"Svetlana",
"Anastasiya",
"Eva",
"Olga"
};

    private static string[] namesLast = new string[]
{
"Williams",
"Black",
"Clark",
"Young",
"Graham",
"Spencer",
"McDonald",
"Warren",
"Dubois",
"Durand",
"Moreau",
"Lefevre",
"Fournier",
"Mercier",
"Martine",
"Girard",
"Garcia",
"Lopez",
"Gonzalez",
"Perez",
"Torres",
"Diaz",
"Flores",
"Hernandez",
"Schmidt",
"Wagner",
"Schulz",
"Klein",
"Werner",
"Braun",
"Weiss",
"Neumann",
"Li",
"Huang",
"Wang",
"Guo",
"Chen",
"Zhou",
"Yang",
"Shen",
"Watanabe",
"Kobayashi",
"Yamaguchi",
"Murakami",
"Sakamoto",
"Nakajima",
"Aoki",
"Ikeda",
"Kim",
"Lee",
"Park",
"Choi",
"Yoon",
"Kwan",
"Shin",
"Han",
"Sharma",
"Chopra",
"Kapur",
"Subramanium",
"Patel",
"Mehta",
"Sengupta",
"Singh",
"Suleiman",
"Farouk",
"Hashim",
"Rahman",
"Asad",
"Khoury",
"Wahid",
"Aziz",
"Bengu",
"Chikunga",
"Okafor",
"Ngele",
"Muthambi",
"Okeke",
"Nwosu",
"Mazibuko",
"Bianchi",
"Giordano",
"De Luca",
"Lombardi",
"Moretti",
"Gasparatto",
"Lazarro",
"Ferrari",
"Ivanov",
"Zinchenko",
"Vasilyev",
"Sokolov",
"Petrov",
"Dobrynin",
"Sidorov",
"Ilyushin"
};

    public static string getNewName()
    {
        string firstName = namesFirst[Random.Range(0, namesFirst.Length)];
        string lastName = namesLast[Random.Range(0, namesLast.Length)];
        return (firstName + " " + lastName);
    }

}
