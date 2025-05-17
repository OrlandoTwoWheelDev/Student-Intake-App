# Student Intake App

A simple .NET 8 application to manage student intake data.

## Day 1

I received the code challenge and started working on it after some initial research.

***Turns out*** there’s a lot I *don’t* have to build from scratch with C#/.NET. I expected to spend hours setting up configs like in TypeScript, Vite, or Node — but nope! Just making sure the right packages were installed and everything was running was enough. I may have shed a joyful tear!

Now that the setup is done, I can fully focus on what matters: learning and implementing the challenge. My “setup” day is officially complete — time to hustle on the code and knowledge tomorrow! 💪🧠🤩

## Day 2

So, this has been quite the eye opener! So far I didnt need to spend hours setting up the database functions to insert into it, 
or carefully crafting the seed file to make sure it works with the database.

Instead i learned about how Entity Framework has these migrations baked in, allowing me to create all aspects of the project with minimal coding. The caveat is that the need to
understand how it works, and WHY it does this so well. Which to me is the fun part! I've basically been given new shiney tools to play with!

So today I designed the database, configured its connection and implemented it in the student intake from with validations and model binding.  I then tested and found that my address2 HAD to be filled in, so a quick adjustment to the 
model let me be able to make it nullable, but that led to a wall of errors. With those errors, I saw how fixing it to be nullable in the model was fine, but I had to make sure the database was updated to reflect that change. With 
that fixed, I tested again and it worked!

I do really enjoy the type safety here in C#, as it mirrors that of TypeScript! I also see it adds another layer, or two, to ensure accuracy and speed of development.

## Day 3

Doing a quick power hour to gain some more understanding of the EF environment. Starting out the day I ran into a Docker problem. 
Docker issue:
- would not open when clicking the icon or even as 'run as admin'. 
- when it did open, it would show 'docker engine stopped'.
- I tried:
	- restarting the gui
	- clearing the cache
	- shutting down the wsl 
	- uninstalling and reinstalling docker
	- adjusting the docker file
With all of that it showed one time something about a system-level issue. So at that point, with about 25 minutes worth of trying,
I decided to forgo using Docker for the project.

In this power hour, I realized I created the form for student intake but didn't have the password field. So, I learned the intricacies 
of how to implement a password field in this language! I like how clean and structured it is, while also allowing speed of development!
I think the biggest part of learning this language on the fly is that (to me) it is very similar to TypeScript, and there is the awesome part 
of having a template to work with, instead of creating every little thing from scratch!

With that said, the power hour was a eye opening success! Time to reflect and rest! 