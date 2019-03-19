// A2.cpp : This file contains the 'main' function. Program execution begins and ends there.
//als Abgabe: das Tool(nicht irgendwelche Binärdateien??), Ausgabedatei (Umleitung von STDOUT von zumindest 25)
//und ein PDF n aufgetragen gegen die Zeit
//Implement a program that accepts an integer via command line option -n and 
//prints instructions to STDOUT as follows:
//Move disk from X to Y
//Likewise, the tool should print the total number of disc move operations to 
//STDERR upon finishing the computation.Allow the user to obtain some information
//on your tool via a --help option.The program should be named as
//* $githubusername - TowersOfHanoi.$suffix - n[--help]
//Measure the runtime of your tool, ensuring that STDOUT is redirected to a 
//file rather than displayed via the console(which would unnecessarily blow up runtime).
//You might employ the concept of subshells to get this done.
//Plot the runtime of your program(in seconds) vs size of the Hanoi puzzle and 
//create a PDF graph.Your pull request should include the following files
//STDERR: gesamtanzahl der moves
//STDOUT: move from peg ... to peg, textausgabe
//runtime in extra file umleiten


#include "pch.h"
#include <iostream>
#include <string>
using namespace std;

void towers(int, char, char, char);
int counter=0;

void printHelp() 
{
	cout << "Help:" << endl << "-n: print the sum of moves for n disks" << endl;
	cout << "Example: $githubusername-TowersOfHanoi.$suffix -n [--help]" << endl;
}

int main(int argc, char const *argv[])
{
	int argv2;
	if (argc <= 2)
	{
		//Warum? 
		//entweder man gibt -n ohne wert an (ist blödsinn -> daher help)
		//oder man gibt --help an, dann soll help auch anzeigt werden
		//oder man gibt tatsächlich nur --all ein, was auch blödsinn wäre. 
		printHelp();
		return 0;
	}
	if (argc > 3)
	{
		printHelp();
		return 0;
	}
	if (argc == 3)
	{
		string argv1 = argv[1];
		if (argv1.compare("-n") == 0)
		{
			//nun hole ich mir die Zahl nach dem Parameter --n 
			argv2 = stoi(argv[2]); //Exception handling?
		}
	}
	else
	{
		printHelp();
	}
	cout << "The sequence of moves for " << argv2 << " disks is: " << endl;
	towers(argv2, 'A', 'C', 'B');
	cerr << "The sum of moves for " << argv2 << " disks is: " << counter << endl;
	return 0;
}

void towers(int num, char frompeg, char topeg, char auxpeg)
{
	if (num == 1)
	{
		cout << "Move disk 1 from peg " << frompeg << " to peg " << topeg << endl; //STDOUT
		counter++;
		return;
	}
	towers(num - 1, frompeg, auxpeg, topeg);
	cout << "Move disk " << num << " from peg " << frompeg << " to peg " << topeg << endl;
	counter++;
	towers(num - 1, auxpeg, topeg, frompeg);
}




// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
