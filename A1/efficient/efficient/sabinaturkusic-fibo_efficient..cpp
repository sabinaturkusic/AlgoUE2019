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
using namespace std;

int main()
{
	int n, t1 = 0, t2 = 1, nextTerm = 0;

	cout << "Enter the number of terms: ";
	cin >> n;
	cout << "Fibonacci Series: ";

	for (int i = 1; i <= n; ++i)
	{
		// Prints the first two terms.
		if (i == 1)
		{
			cout << " " << t1;
			continue;
		}
		if (i == 2)
		{
			cout << t2 << " ";
			continue;
		}
		nextTerm = t1 + t2;
		t1 = t2;
		t2 = nextTerm;

		cout << nextTerm << " ";
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
