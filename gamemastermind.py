from optparse import check_choice
import random 

COLORS = ["R", "G", "B", "Y", "W", "O"]
TRIES = 10 
CODE_LENGTH = 4 


def generate_code():
    code = []

    for _ in range(CODE_LENGTH):
        color = random.choice(COLORS)
        code.append(color)

    return code

def guess_code():

    while True:
        guess = input ("Guess: ").upper().split(" ")

        if len(guess) != CODE_LENGTH:
            print(f"You must guess{CODE_LENGTH} colors. ")
            continue
        for color in guess:
            if color not in COLORS:
                print(f"Invalid color: {color}. Try again.")
                break
            else:
                break

        return guess
    
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
                color_counts[guess_color ] += 1 


        for guess_color, real_color in zip(guess, real_code):
            if guess_color in color_counts and color_counts[guess_color] > 0:
                incorrect_pos += 1 
                color_counts[guess_color] -= 1
                
        return correct_pos, incorrect_pos
def game():
    print(f"Welcome to mastermind, you have {TRIES} to guess the code..")
    print("The valid colors are", *COLORS)

    code = generate_code()
    for attempts in range(1, TRIES + 1 ):
        guess = guess_code()
        correct_pos, incorrect_pos = check_choice(guess, code)

        if correct_pos == CODE_LENGTH:
            print(F"You guessed the code in {attempts} tries!")
            break
    
        print(f"Correct Positions:{correct_pos} | Incorrect Positions: {incorrect_pos}")
    
    else:
        print("You ran out of tries, the code was:", *code)

if __name__ == "__main__":
    game()

#Description of my Mastermind game
#Generate a random code, 4 colors, users has to choose between the six colors that are part of the game
#Make the user guess the code, make sure what user is guessing is valid, if not will provide feedback
#Compare the guess, determine the number of correct colors thgat are in the correct position and the numbers of colors that are in the code that are in the incorrect position   
#Tie the game together, 10 guesses, constantly give output whether right or wrong, determine if user got the correct code and determine if user lost the game.



    
    

        

        

        


                

    
   

        
        
