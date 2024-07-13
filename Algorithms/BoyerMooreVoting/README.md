# Boyer Moore - majority vote algorithm

## Overview

The project implements the Boyer-Moore Voting Algorithm to find the majority element in a collection of elements.
<br/>
It includes generic implementations, unit tests using xUnit, and a detailed test report.
<br/>
The idea for the project came from a question that appeared in [LeetCode]("https://leetcode.com/problems/majority-element-ii/description/").

## Highlights 

- Implemented the Boyer-Moore Voting Algorithm in C#.
- Created generic methods to handle various data structures (arrays, lists, sets).
- Developed unit tests using xUnit to validate algorithm correctness.
- Added error handling and result reporting using a custom `Result<T>` class.
- Enhanced test cases to cover edge cases and error scenarios.

## Space and Time Complexity

- **Time Complexity**: O(n) - where n is the number of elements in the collection.
 <br/>The algorithm makes a single pass through the collection to identify a potential majority element and a second pass (optional) to verify it.
 <br/>

- **Space Complexity**: O(1) - the algorithm uses a constant amount of additional space, regardless 
of the input size.

## Test Results

- Successfully validated majority element detection across different data structures (arrays, lists, sets).
- Handled edge cases such as empty collections and scenarios with no majority element.

## Conclusion

The project demonstrates implemented  the use of algorithms, generics, and unit testing in C#. 

  [![C#](https://img.shields.io/badge/-C%23-333333?style=flat&logo=CSharp&logoColor=7e10cc)](https://github.com/Ido-Saroka/CSharp)