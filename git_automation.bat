set /p message="Commit message: "


git diff
git add .
git commit -m "%message%"
git push origin master
git push github master