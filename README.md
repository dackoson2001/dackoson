Add files via upload


1. **Imports and Constants:**
```python
from optparse import check_choice
import random 

COLORS = ["R", "G", "B", "Y", "W", "O"]
TRIES = 10 
CODE_LENGTH = 4
```

- `random` is imported to generate random selections of colors.
- `COLORS` defines the six valid colors that the player can choose from: Red (R), Green (G), Blue (B), Yellow (Y), White (W), and Orange (O).
- `TRIES` sets the maximum number of attempts a player has to guess the correct code, which is 10.
- `CODE_LENGTH` sets the length of the hidden code, which is 4.

### 2. **`generate_code()` Function:**
```python
def generate_code():
    code = []
    for _ in range(CODE_LENGTH):
        color = random.choice(COLORS)
        code.append(color)
    return code
```
- This function generates a random 4-color code.
- It selects a color at random from the `COLORS` list for each position in the code (with a length of `CODE_LENGTH` which is 4).
- The generated code is returned as a list of colors.

3. **`guess_code()` Function:**
```python
def guess_code():
    while True:
        guess = input("Guess: ").upper().split(" ")
        if len(guess) != CODE_LENGTH:
            print(f"You must guess {CODE_LENGTH} colors.")
            continue
        for color in guess:
            if color not in COLORS:
                print(f"Invalid color: {color}. Try again.")
                break
            else:
                break
        return guess
```
- This function is responsible for obtaining the player's guess.
- It prompts the player to input their guess, which must consist of 4 colors (separated by spaces).
- The input is converted to uppercase and split into a list of individual colors (`split(" ")`).
- The function checks if the guess has the correct number of colors (4). If not, it prompts the user again.
- It then checks if each color in the guess is valid (i.e., it exists in the `COLORS` list). If an invalid color is entered, the function will ask the player to try again.
- Once the guess is valid, it returns the guess as a list of string
-
- 4. **`check_code()` Function:**
```python
def check_code(guess, real_code):
    color_counts = {}
    correct_pos = 0
    incorrect_pos = 0

    for color in real_code:
        if color not in color_counts:
            color_counts[color] = 0
        color_counts[color] += 1

    for guess_color, real_color in zip(guess, real_code):
        if guess_color == real_color:
            correct_pos += 1
            color_counts[guess_color] += 1

    for guess_color, real_color in zip(guess, real_code):
        if guess_color in color_counts and color_counts[guess_color] > 0:
            incorrect_pos += 1
            color_counts[guess_color] -= 1

    return correct_pos, incorrect_pos
```
- This function compares the player's guess with the actual (hidden) code.
- `color_counts` is a dictionary that tracks the count of each color in the real code.
- It first loops through the real code and counts how many times each color appears.
- Then it compares the player's guess with the real code, checking for exact matches (correct position and color). For each exact match, `correct_pos` is incremented.
- After checking for exact matches, it counts the number of colors in the guess that are in the code but not in the correct position (incorrect position).
- Finally, it returns the count of colors in the correct position (`correct_pos`) and the count of colors in the incorrect position (`incorrect_pos`).

5. **`game()` Function:**
```python
def game():
    print(f"Welcome to mastermind, you have {TRIES} to guess the code..")
    print("The valid colors are", *COLORS)
    code = generate_code()
    for attempts in range(1, TRIES + 1):
        guess = guess_code()
        correct_pos, incorrect_pos = check_choice(guess, code)
        if correct_pos == CODE_LENGTH:
            print(F"You guessed the code in {attempts} tries!")
            break
        print(f"Correct Positions:{correct_pos} | Incorrect Positions: {incorrect_pos}")
    else:
        print("You ran out of tries, the code was:", *code)
```
- This is the main game loop.
- The game begins with a welcome message indicating how many attempts the player has to guess the code.
- A random code is generated using the `generate_code()` function.
- The game allows the player up to 10 guesses (`TRIES`), checking each guess with the `guess_code()` function.
- After each guess, the `check_choice()` function (though incorrectly named, it should be `check_code()`) is used to determine how many colors are in the correct and incorrect positions.
- If the player guesses the code correctly (all positions match), a success message is printed, and the game ends.
- If the player runs out of tries, the code is revealed, and the game ends.

6. **Entry Point:**
```python
if __name__ == "__main__":
    game()
```
- This checks if the script is being run directly (not imported as a module) and calls the `game()` function to start the game.


### **Game Flow:**
1. The program generates a random code of 4 colors.
2. The player has 10 attempts to guess the correct code.
3. After each guess, the game provides feedback:
   - The number of correct positions (exact color and position matches).
   - The number of incorrect positions (correct color but wrong position).
4. The game ends when the player guesses the correct code or runs out of attempts.

### **Player Interaction:**
- The player is prompted to enter a guess (4 colors separated by spaces).
- Feedback is provided after each guess to guide the player toward finding the correct code.

This is a basic implementation of a Mastermind game, please advise
