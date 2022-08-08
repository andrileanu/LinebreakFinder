using snake;
using System.Collections.Generic;


//höhe eingeben
Console.WriteLine("Wie hoch soll das Feld sein? ");
string userInput = Console.ReadLine();

if (InputCheck.IsInt(userInput) == false)
{
    Console.WriteLine("falsche eingabe");
    return;
}
int height = int.Parse(userInput);



if (InputCheck.CheckHight(height) == false)
{
    Console.WriteLine("falsche höhe");
    return;
}






//breite eingeben
Console.WriteLine("Wie breit soll das Feld sein? ");

Console.ReadLine();

if (InputCheck.IsInt(userInput) == false)
{
    Console.WriteLine("falsche eingabe");
    return;
}

int width = int.Parse(userInput);

if (InputCheck.CheckHight(width) == false)
{
    Console.WriteLine("falsche breite");
    return;
}







//einzeile den Blöcken
Console.WriteLine("Wie viele Blöcke willst du haben ?");
int blöcke = int.Parse(Console.ReadLine());

if (InputCheck.IsInt(userInput) == false)
{
    Console.WriteLine("falsche eingabe");
    return;
}

if (InputCheck.CheckWidth(width) == false)
{
    Console.WriteLine("unmöglich");
    return;
}











Console.Clear();

var blocks  = Block.GetBlocks(height, width, blöcke, 3);

blocks.ForEach(block => block.draw());

var snake = new snake_();
var foodfactory = new FoodFactory_();

snake.width = width;
snake.height = height;

foodfactory.width = width;
foodfactory.height = height;

//Snake and Apple 

Console.CursorVisible = false;
//Apfel in der Karte 

//loop for snake, what ate he and when

foodfactory.CreateFood(height, width, snake, blocks );
var edge = new string('_', width + 3);
var edge1 = new string('"', width + 3);
var whall = new string('|', 1);


Console.SetCursorPosition(0, 0);
Console.Write(edge);

Console.SetCursorPosition(0, height + 2);
Console.Write(edge1);

for (int i = 1; i < height + 2; i++) 
{ 
    //rechte Kante
    Console.SetCursorPosition(width + 2, i);
    Console.Write(whall);

    //linke Kante
    Console.SetCursorPosition(0, i);
    Console.Write(whall);
     
}


while (true)
{

    while (Console.KeyAvailable == false)
    {
        if (snake.xHead == foodfactory.x & snake.yHead == foodfactory.y)
        {
            if (snake.Elemente.Count == 100)
            {
                Console.SetCursorPosition(0, height + 2);

                Console.Write("Bravo");
            }

            snake.eat();
            foodfactory.CreateFood(height, width, snake, blocks);
        }

        Thread.Sleep(250);

        if (!snake.Move(blocks))
        {

            return;

        }
    }

    // jemand hat was gedrückt.
    char ch = Console.ReadKey(true).KeyChar;


    //typing
    snake.turn(ch);
}



//Aplle on the map and her random


public enum Directions
{
    down,
    up,
    left,
    right,
}