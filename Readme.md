The implementation of task listed in task.md file:

The project partially done in TDD way.
I only implemented players in this way.

Didn't test container and multitask stuff for save time. But can add tests there if needed.

The application has prelisted players and player types to looking like random players participating to playing.

Also I'm not strongly split multithreading engine also for save time - but it is possible to refactor this in more proper
way.

Mostly I have players - which do guessing in own system,
Player container - which count attempts, penalty, total penalty and making all checkings for each players.
Any situation like player is guessed number, reach total penalty or total attempts is inform hub and stop guessing at all.

When number is guessed - rest players is stop guessing too.

I'm used TPL for making multithreading but it's possible to use ThreadPool if needed.