// See https://aka.ms/new-console-template for more information



// Sweet ‘n Salty .NET console application 
// Summary: 
// The Sweet and Salty Console Application prints numbers to the console screen starting at a number specified by the 
// user and stopping at a number specified by the user.  
// The range of the two numbers is limited to 1000.  
// The quantity of numbers printed per line is also decided by the user with a maximum of 30. 
// The application will print “Sweet” instead of any number that is a multiple of 3. 
// The application will print “Salty” instead of any number that is a multiple of 5. 
// The application will print “Sweet’nSalty” instead of any number that is a multiple of both 3 and 5. 
// The application prints a summary after completing the printout. 




int start, end;

bool exit = false;
while (!exit)
{
    System.Console.WriteLine("Start:");
    if (int.TryParse(Console.ReadLine(), out start))
    {
        if (start < 1000)
        {
            System.Console.WriteLine("End:");
            if (int.TryParse(Console.ReadLine(), out end))
            {
                if (end > start && end <= 1000)
                {

                }
                else{
                    System.Console.WriteLine("end must be greater than start and ");
                }
            }
        }
    }
}
