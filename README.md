# GMTK-game-jam-2022

Roll of the dice

cuphead - dice themed
mario party - actual dice, but not 1-6. Still some active control. skill element to get the right element
    casino cheating - somehow make the dice not random
        weighted dice
        magnets
        *dice with different faces*
        risk reward
dicey dungon - dice roguelike, dice are the weapons, *upgrading dice*



dungeon dice monsters - *unfolding dice*

platformer, dice become land
combat, dice become attacks - real time - must roll the dice

lost kingdoms - dice are your allies, do different things, support effects (sleep)
- roguelike, purchase new dice, some dice have attacks, some are mixes, high risk high reward
- chain of memories managent,
- only 3 dice into any one battle
- combat room encounters

first die
- 6 sided, quick attacks
second room
- hyper beam, long
third room
- summon die
fourth room
- foruth die, swap out

MVP curated levels. 

room
    camera style, binding of isaac style
    your current dice in the bottom left
controls
    keyboard and mouse
    "using" a die is drag and actually rolling
    dice live in the hud. Where you release is where the die appears in the real world. sword - swipe, lunge, depending on randomness
        alternatively dice could have some initial roll. not sure what die we're rolling. not 
    mvp no colliders
    extention inate properties
    enemies - white boxes - melee enemies run around try to touch youa
    When you start a new room, dice are in a fixed position
UI
    health - hearts, reset between rooms
    death screen 
    win - door opens, takes you to next level
    dice selection menu
        dice award screen curated the entire way when you kill last enemy
        yay shiney new dice in middle. left, currently equipt dice, right unequipt dice, when you select 3 or fewer, then you close the menu and procceed to the next level

First sprint
    DONE: Create the room (Mat)
    DONE: Player + movement controller (Mat)
    DONE: Single melee enemy + AI (walk to player)
    DONE: heath system (Franklin) (player and enemy)
    
    DONE: failure state (text that says you lose + reset button) (Franklin)
    DONE: success state (text that says you win + reset button) (Franklin)
    DONE: dice that you can roll
    	Switch to mouse interaction (Mat)
	DONE: Animations (Franklin)
    DONE: faces that trigger differnt actions
        single sword die, literally number 1-6

    NPE bug on dice check
    dice skin
    change swipe angle on 6 to 179
    make the swipe animation follow the player

Second sprint:
    Reward Menu: second die
        menu screens
        drag and drop
        Second die, all 360 1 damage sides
    Dice manager
        Dice management that works with multiple dice
        use existing dice drag manager
    Door to open, walk through trigger second level
    
    room manager
        list of enemies and spawn positions
        second room twice as big, 2 enemies
        add endgame menu
        update reward/defeat menus
    