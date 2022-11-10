## None
- Create sub graph V(G)-R
- BFS

## Some
- For all r in R
- Run flow where r is source and s and t are sinks

## Many
- Longest path?

## Few
- Add directed edge weights
  for all edges (v,w) where w in R => weight=1 and weight=1 otherwise
- Dijkstra

## Alternate
- Create sub graph of E(G) v,w where v in R and w in V(G)-R or v in V(G)-R and w in R
- BFS
