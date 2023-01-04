from decimal import Decimal
import sys
import math

# 1. Transform problem to 1 dimension using pythagoras
# 2. Sort on left
# 3. keep watered
# 4. Find largest current right while curr left <= watered

def best_between(i, left, intervals):
  best = (-1, -1)
  while i < len(intervals) and intervals[i][0] <= left:
    best = best if best[1] > intervals[i][1] else intervals[i]
    i += 1
  return (i, best)

def solve(intervals, L):
  idx = 0
  left = 0
  right = L

  count = 0
  while (left < right):
    (i, best) = best_between(idx, left, intervals);
    idx = i
    if (best[0] == -1 and best[1] == -1):
      return -1
    count += 1
    left = best[1]

  return count

for line in sys.stdin:
  N, L, W = map(int, line.split())
  intervals = []

  for i in range(N):
    x, r = map(int, input().split())
    pW = Decimal(W)
    px = Decimal(x)
    pr = Decimal(r)
    p2 = Decimal(2)
    side_length = Decimal(math.sqrt(Decimal(pr**2) - Decimal((pW/p2)**2)));
    left = px-side_length;
    right = px+side_length;
    intervals.append((left, right));

  intervals.sort()

  print(solve(intervals, L))
