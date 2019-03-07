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

main()
{
	int n, c, first = 0, second = 1, next;

	cout << "Enter the number of terms of Fibonacci series you want" << endl;
	cin >> n;

	cout << "First " << n << " terms of Fibonacci series are :- " << endl;

	for (c = 0; c < n; c++)
	{
		if (c <= 1)
			next = c;
		else
		{
			next = first + second;
			first = second;
			second = next;
		}
		cout << next << endl;
	}

	return 0;
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