using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class EmailScreenController : MonoBehaviour {

	public GameObject EmailLine;

	public Transform EmailLinesStart;

	public int MaxEmailsOnScreen;

	private List<GameObject> EmailLines;

	private int currentCursorLocation;
	private int currentLevelNum;

	private int scrollOffset;

	private const string EMAIL_DATA_CONFIG_DIRECTORY = "Assets/Configuration/Email Flow.txt";

	private List<List<Email>> emails;

	void Initialize() {
		emails = new List<List<Email>>();
		List<Email> currentLevelEmailList = null;
		StreamReader sr = new StreamReader(EMAIL_DATA_CONFIG_DIRECTORY);
		string line;
		while((line=sr.ReadLine()) != null) {
			//& is new level
			//* is new email
			if(line != "") {
				if(line[0] == '&')
				{
					//We have a new level, so create a new list of emails and push the old list
					if(currentLevelEmailList != null) {
						emails.Add(currentLevelEmailList);
					}
					currentLevelEmailList = new List<Email>();
				} else if(line[0] == '^')
				{
					//We have a new email, so grab the usefule info from the next couple of lines
					sr.ReadLine();
					string date = sr.ReadLine();
					sr.ReadLine();
					string from = sr.ReadLine();
					sr.ReadLine();
					string subject = sr.ReadLine();
					sr.ReadLine();
					string body = sr.ReadLine();
					
					if(currentLevelEmailList != null) {
						currentLevelEmailList.Add(new Email(subject,body,from,date));
					}
				}
			}
		}

		if(currentLevelEmailList != null) {
			emails.Add(currentLevelEmailList);
		}
	}

	void LoadToLevel(int levelNum) {
		currentLevelNum = levelNum;

		int totalEmails = 0;
		currentCursorLocation = 0;
		List<Email> latestEmails;
		Vector3 startingLoc = EmailLinesStart.position;
		EmailLines = new List<GameObject>();

		while(currentLevelNum >= 0 && totalEmails < MaxEmailsOnScreen) {
			latestEmails = emails[currentLevelNum];
			int numEmails = latestEmails.Count;
			int currentEmail = 0;
			while(totalEmails < MaxEmailsOnScreen && currentEmail < numEmails) {
				Email email = latestEmails[currentEmail];
	         	GameObject newLine = Instantiate(EmailLine, startingLoc, Quaternion.identity) as GameObject;
	          	newLine.transform.parent = transform;
	           	newLine.SendMessage("SetEmail", email);
	          	//Email height homie!
	           	startingLoc.y -= 1;
				totalEmails += 1;
				currentEmail += 1;
				EmailLines.Add(newLine);
			}
			levelNum -= 1;
		}
		EmailLines[currentCursorLocation].SendMessage("Activate");
	}

	void reloadEmails() {
		int offset = scrollOffset;
		int totalEmails = 0;
		List<Email> latestEmails;
		Vector3 startingLoc = EmailLinesStart.position;
		while(currentLevelNum >= 0 && totalEmails < MaxEmailsOnScreen) {
			latestEmails = emails[currentLevelNum];
			int currentEmail = 0;
			int numEmails = latestEmails.Count;
			while(totalEmails < MaxEmailsOnScreen && currentEmail < numEmails) {
				if(offset == 0) {
					Email email = latestEmails[currentEmail];
					GameObject line;
					if(EmailLines.Count > totalEmails) {
						line = EmailLines[totalEmails];
					} else {
						startingLoc.y -= totalEmails;
						line = Instantiate(EmailLine, startingLoc, Quaternion.identity) as GameObject;
						line.transform.parent = transform;
						EmailLines.Add(line);
					}
					line.SendMessage ("SetEmail",email);
					totalEmails += 1;
				} else {
					offset -= 1;
				}
			}
		}

	}

	void MoveCursor(EmailNavigationArrowController.ArrowDirection direction) {
		EmailLines[currentCursorLocation].SendMessage("Deactivate");
		int oldOffset = scrollOffset;
		if(direction == EmailNavigationArrowController.ArrowDirection.Up) {
			currentCursorLocation -= 1;
			if(currentCursorLocation < 0) {
				currentCursorLocation = 0;
				scrollOffset -= 1;
				scrollOffset = scrollOffset < 0 ? 0 : scrollOffset;
			}

		} else {
			currentCursorLocation += 1;
			if(currentCursorLocation > MaxEmailsOnScreen) {
				currentCursorLocation = MaxEmailsOnScreen;
				scrollOffset += 1;
				scrollOffset = scrollOffset >= EmailLines.Count ?  EmailLines.Count - 1 : scrollOffset;
			}
		}

		if(scrollOffset != oldOffset) {
			reloadEmails();
		}
		EmailLines[currentCursorLocation].SendMessage("Activate");
	}
}
