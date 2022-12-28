T = int(input())
for t in range(T):
  N = int(input())
  peop = []
  for n in range(N):
    Ws = list(map(int, input().split()))
    W = Ws[0]
    Wi = Ws[1:]
    peop.append(sum(Wi))
  peop.sort()
  wait = []
  time = 0
  for p in peop:
    wait.append(time + p)
    time += p
  print(sum(wait) / len(wait))