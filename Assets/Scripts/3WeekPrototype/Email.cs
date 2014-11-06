using UnityEngine;
using System.Collections;

public class Email {

	public string subject;
	public string body;
	public string from;
	public string date;

	public Email(string subject, string body, string from, string date) {
		this.subject = subject;
		this.body = body;
		this.from = from;
		this.date = date;
	}
}
