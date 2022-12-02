N, t = map(int, input().split())
A = list(map(int, input().split()))

def dostuff(A):
  if t == 1: return 7
  if t == 2:
    a0 = A[0]
    a1 = A[1]
    if a0 > a1:  return "Bigger"
    if a0 == a1: return "Equal"
    return "Smaller"
  if t == 3:
    return sorted([A[0], A[1], A[2]])[1]
  if t == 4:
    return sum(A)
  if t == 5:
    s = 0
    for i in A:
      if i%2 == 0:
        s += i
    return s
  if t == 6:
    return ''.join(map(lambda x: chr(97+(x%26)), A))
  if t == 7:
    i = 0
    seen = set({})
    while True:
      if i == len(A)-1: return "Done"
      if i >  len(A)-1: return "Out"
      seen.add(i)
      i = A[i]
      if i in seen: return "Cyclic"

print(dostuff(A))