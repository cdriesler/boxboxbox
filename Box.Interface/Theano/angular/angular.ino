/*
  Button

  Turns on and off a light emitting diode(LED) connected to digital pin 13,
  when pressing a pushbutton attached to pin 2.

  The circuit:
  - LED attached from pin 13 to ground
  - pushbutton attached to pin 2 from +5V
  - 10K resistor attached to pin 2 from ground

  - Note: on most Arduinos there is already an LED on the board
    attached to pin 13.

  created 2005
  by DojoDave <http://www.0j0.org>
  modified 30 Aug 2011
  by Tom Igoe

  This example code is in the public domain.

  http://www.arduino.cc/en/Tutorial/Button
*/

#include <Servo.h>

Servo servoBot;
Servo servoMid;
Servo servoTop;
// twelve servo objects can be created on most boards

int pos = 0;    // variable to store the servo position

// constants won't change. They're used here to set pin numbers:
const int button0 = 5;     // the number of the pushbutton pin
const int button1 = 2;
const int button2 = 3;
const int button3 = 4;
const int button4 = 9;
const int button5 = 6;
const int button6 = 7;
const int button7 = 8;

// variables will change:
int b0State = 0;
int b1State = 0;
int b2State = 0;
int b3State = 0;
int b4State = 0;
int b5State = 0;
int b6State = 0;
int b7State = 0;

bool increasing = true;

void setup() {
  Serial.begin(9600);

  // inititalize servos
  servoBot.attach(12);
  servoMid.attach(11);
  servoTop.attach(10);
  
  // initialize buttons
  pinMode(button0, INPUT);
  pinMode(button1, INPUT);
  pinMode(button2, INPUT);
  pinMode(button3, INPUT);
  //pinMode(button4, INPUT);
  //pinMode(button5, INPUT);
  //pinMode(button6, INPUT);
  //pinMode(button7, INPUT);
}

void loop() {
  // read the state of the pushbutton value:
  b0State = digitalRead(button0);
  b1State = digitalRead(button1);
  b2State = digitalRead(button2);
  b3State = digitalRead(button3);
  //b4State = digitalRead(button4);
  //b5State = digitalRead(button5);
  //b6State = digitalRead(button6);
  //b7State = digitalRead(button7);

  Serial.print(b0State);
  Serial.print(" / ");
  Serial.print(b1State);
  Serial.print(" / ");
  Serial.print(b2State);
  Serial.print(" / ");
  Serial.print(b3State);
  Serial.println();

/*
  // check if the pushbutton is pressed. If it is, the buttonState is HIGH:
  if (buttonState == HIGH) {
    // turn LED on:
    digitalWrite(ledPin, HIGH);
  } else {
    // turn LED off:
    digitalWrite(ledPin, LOW);
  }
*/

  if (increasing == true) {
    pos += 2;

    servoBot.write(pos);              // tell servo to go to position in variable 'pos'
    servoMid.write(pos);
    servoTop.write(pos);
    delay(300); 

    if (pos >= 180) {
      increasing = false;
    }
  }
  else {
    pos -= 2;

    servoBot.write(pos);              // tell servo to go to position in variable 'pos'
    servoMid.write(pos);
    servoTop.write(pos);
    delay(80); 

    if (pos <= 0) {
      increasing = true;
    }
  }
}
