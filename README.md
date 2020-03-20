# GitStory
Git Story - save all your changes to git

The target of that project is to provide a way to track all your changes in git.
There are different styles of commiting - random commits, long commits, mini commits - every developer grow his own style; generally commits should be as small as possible, but that library promotes micro commits - save every your save to git. It produce a lot of commits even on a small reasonable commit - 10-15 commits, 5 minimum for even a small change in the IDE (definitely need a filter).

So my library consisting of a IDE extension (only VS17-19 now) commits every IDE save to separate branch parallel to the current. Usually commits consist of several files but some operations in the IDE makes micro saves.

So the picture looks like this:

![](doc/20-03-2020.21-59-01.png)

Parallel to the main branch there is a {branch}_{sha}_change branch with all micro-commits. The commit message is stupid, i was expecting git client showing time between commits but as commits age you have no luck restoring it visually. 

I find it interesting tool of getting back to context of any of my numerous projects, some of them abandoned on a midst of the moment of implementation; what happened long time ago need to be particularly thorough.

All this commits hold the information of your last coding actions on any project, task or fix, so you potentially restore the context of your mind process at any given point in the past and find the changes rejected by current task.