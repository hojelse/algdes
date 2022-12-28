#include "../header.hpp"
#include <string>
#include <map>

vi idxs(string cs, string pattern) {
  vi idxs;
  ll idx = 0;
  for (ll i = 0; i < cs.size(); i = idx+1) {
    idx = cs.find(pattern, i);
    if (idx == -1) break;
    idxs.push_back(idx);
  }
  return idxs;
}

void print_row(const string& row) {
  for (ll i = 0; i < 6; i++) {
    for (ll j = 0; j < 10; j++) {
      cout << row[(i*10)+j];
    }
    cout << endl;
  }
}

void print_col(const string& row) {
  for (ll i = 0; i < 10; i++) {
    for (ll j = 0; j < 6; j++) {
      cout << row[(i*6)+j];
    }
    cout << endl;
  }
}

string mirror_row(const string& row) {
  string col = "#############################################################";
  ll idx = 0;
  for (ll j = 0; j < 10; j++) {
    for (ll i = 0; i < 6; i++) {
      col[idx++] = row[(i*10)+j];
    }
  }
  return col;
}
string mirror_col(const string& col) {
  string row = "#############################################################";
  ll idx = 0;
  for (ll j = 0; j < 6; j++) {
    for (ll i = 0; i < 10; i++) {
      row[idx++] = col[(i*6)+j];
    }
  }
  return row;
}

string replace(string s, ll idx, string pattern) {
  for (ll i = 0; i < 3; i++) {
    s[idx+i] = (pattern[i] == '.') ? 'o' : '.';
  }
  return s;
}

ll rec(string row, ll count, map<string, ll>& cache) {
  if (cache.find(row) != cache.end())
    return cache[row];
  vi counts;

  for (ll i : idxs(row, "oo.")) counts.push_back(rec(replace(row, i, "oo."), count-1, cache));
  for (ll i : idxs(row, ".oo")) counts.push_back(rec(replace(row, i, ".oo"), count-1, cache));
  string col = mirror_row(row);
  for (ll i : idxs(col, "oo.")) counts.push_back(rec(mirror_col(replace(col, i, "oo.")), count-1, cache));
  for (ll i : idxs(col, ".oo")) counts.push_back(rec(mirror_col(replace(col, i, ".oo")), count-1, cache));

  ll best = count;
  for (ll c : counts)
    best = min(best, c);

  cache[row] = best;
  return best;
}

void solve() {
  string row = "#############################################################";
  char c;
  ll count = 0;
  for (ll i = 0; i < 5; i++) {
    for (ll j = 0; j < 9; j++)
    {
      cin >> c;
      if (c == 'o') count++;
      row[(i*10)+j] = c;
    }
  }

  map<string, ll> cache;

  ll best_count = rec(row, count, cache);

  cout << best_count << " " << count - best_count << endl;
}

int main() {
  ll N;
  cin >> N;

  for (ll i = 0; i < N; i++){
    solve();
  }

  return 0;
}