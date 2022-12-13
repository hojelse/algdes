# -Wall show all warnings. It turns on all standard C++ warnings about code that might cause unexpected or undefined behavior.
# -g provides debugging feature for your program. You will need this when you want to use gdb.
# -O2 optimize code and check for errors in parallel to find potential bugs.
# --std=c++<##> uses version <##> of C++ when compiling. This will allow you to use specific features of that C++ version.
CFLAGS = -Wall -g -O2 -std=gnu++17

%: %.cpp
	g++ ${CFLAGS} $< -o $@.out

clean:
	rm **/*.out; rm -r **/*.out.*
