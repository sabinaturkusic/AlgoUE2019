// efficient.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
//fibonacci(n)
// F1 <- 1
// F2 <- 2
//for i <- 3 to n do
//	Fi <- Fi-1 + Fi-2
//end for
//return Fn

#include "pch.h"
#include <iostream>
#include <algorithm>
#include <string>
#include <vector>
using namespace std;

vector<int> vectorResults;

int Fibonacci(int n)
{
	int t1 = 1;
	int t2 = 1;
	int nextTerm = 0;

	for (int i = 0; i <= n; ++i)
	{
		// Prints the first two terms.
		if (i == 1)
		{
			vectorResults.push_back(1);
		}
		if (i == 2)
		{
			vectorResults.push_back(2);
			continue;
		}
		nextTerm = t1 + t2;
		vectorResults.push_back(t1);
		t1 = t2;
		t2 = nextTerm;

	}
	return 0;

}

void printHelp() {
	cout << "Help:" << endl << "-n: print the nth Fibonacci number"
		<< endl << "--all: print all Fibonacci numbers up to n" << endl;
	cout << "Example: $githubusername-fibo_inefficient.$suffix -n [--all] [--help]" << endl;
}

void calcFibonnaci(int n, bool allPrint) //hier wird mit boolschen operatoren und all alle ausgegeben
{
	Fibonacci(n);
	sort(vectorResults.begin(), vectorResults.end());
	vectorResults.erase(unique(vectorResults.begin(), vectorResults.end()), vectorResults.end());

	if (allPrint)
	{
		cout << "1" << endl;

		for (auto const&c : vectorResults)
		{
			cout << c << endl;
		}
	}
	else
	{
		int size = vectorResults.size();
		cout << vectorResults[size - 1] << endl;
	}
}

int main(int argc, char const *argv[])
{
	if (argc <= 2)
	{
		//Warum? 
		//entweder man gibt -n ohne wert an (ist blödsinn -> daher help)
		//oder man gibt --help an, dann soll help auch anzeigt werden
		//oder man gibt tatsächlich nur --all ein, was auch blödsinn wäre. 
		printHelp();
		return 0;
	}
	if (argc > 4)
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
			int argv2 = stoi(argv[2]);  //Exception handling? 
			calcFibonnaci(argv2, false);
		}
		else
		{
			printHelp();
		}
	}
	if (argc == 4)
	{
		string argv1 = argv[1];
		string argv3 = argv[3];
		if (argv1.compare("-n") == 0)
		{
			//nun hole ich mir die Zahl nach dem Parameter --n 
			int argv2 = stoi(argv[2]); //Exception handling?
			if (argv3.compare("--all") == 0)
			{
				calcFibonnaci(argv2, true);
			}
			else
			{
				printHelp();
			}
		}
		else
		{
			printHelp();
		}
	}
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
