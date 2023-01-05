# Stable Marriage
make a perfect bipartite matching, between N proposers to N rejecters

Fully ranked with no ties

## Input

On the first line, a single integer, N

Then followed by N lines where line i is the preference list of proposer i. Each line consists of N integers, each corresponding to a rejector, sorted from left to right, from prefer most to prefer least.

## Output

Output the stable matching on N rows, where line i, is the proposer i then a space then the rejector to which proposer i is engaged.

If multiple stable matchings are possible output any correct solution.

## Samples

Sample Input 1
```
2
0 1
0 1
0 1
0 1
```

Sample Output 1
```
0 0
1 1
```
