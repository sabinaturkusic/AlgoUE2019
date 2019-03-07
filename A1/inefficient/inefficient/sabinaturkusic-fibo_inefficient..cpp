// A1.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
//inefficent  - Laufzeit steigt exponentiell
//time vor programm in konsole aufrufen und dann nmisst es die runtime
#include "pch.h"
#include <iostream>
using namespace std;

//if n=1 or n=2 then
//	return 1
//else 
//a <- recursivefibonacci(n-1)
//b <- recursive fibonacci(n-2)
//	return a+b
//end if

int Fibonacci(int x)
{
	if (x < 2) {
		return x;
	}
	return (Fibonacci(x - 1) + Fibonacci(x - 2));
}

int main ()
{	
	int n;
	cout << "Print the nth Fibonacci number:" << endl;
	cin >> n;
	cout << " Fibonacci is: " << Fibonacci(n) << endl;

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