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

bool newSession = true;

int pos = 0;    // variable to store the servo position
int commits = 0;

int egg = 0;

// constants won't change. They're used here to set pin numbers:
const int button0 = 5;     // the number of the pushbutton pin
const int button1 = 2;
const int button2 = 3;
const int button3 = 4;
const int button4 = 9;
const int button5 = 6;
const int button6 = 7;
const int button7 = 8;

const int commitButton = 13;

// variables will change:
int b0State = 0;
int b1State = 0;
int b2State = 0;
int b3State = 0;
int b4State = 0;
int b5State = 0;
int b6State = 0;
int b7State = 0;

int commitButtonState = 0;

bool increasing = true;

int previousStates[] = {0, 0, 0, 0, 0, 0, 0, 0};
int currentStates[] = {0, 0, 0, 0, 0, 0, 0, 0};
int lastPressed[] = {-1, -1};

int botPlacement[] = {-1, -1};
int midPlacement[] = {-1, -1};
int topPlacement[] = {-1, -1};

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
  pinMode(button4, INPUT);
  pinMode(button5, INPUT);
  pinMode(button6, INPUT);
  pinMode(button7, INPUT);

  pinMode(commitButton, INPUT);
}

void loop() {
  if (egg >= 5) {
    DoEgg();
  }
  // read the state of the pushbutton value:
  b0State = digitalRead(button0);
  b1State = digitalRead(button1);
  b2State = digitalRead(button2);
  b3State = digitalRead(button3);
  b4State = digitalRead(button4);
  b5State = digitalRead(button5);
  b6State = digitalRead(button6);
  b7State = digitalRead(button7);

  for (int i = 0; i < 8; i++) {
    previousStates[i] = currentStates[i];
  }

  currentStates[0] = b0State;
  currentStates[1] = b1State;
  currentStates[2] = b2State;
  currentStates[3] = b3State;
  currentStates[4] = b4State;
  currentStates[5] = b5State;
  currentStates[6] = b6State;
  currentStates[7] = b7State;

  for (int i = 0; i < 8; i++) {
    Serial.print(currentStates[i]);
    Serial.print(" / ");
  }

  Serial.println();

  for (int i=0; i < 8; i++) {
    if (currentStates[i] == 1) {
      if (previousStates[i] != 1 && lastPressed[0] != i && lastPressed[1] !=i) {
        lastPressed[1] = lastPressed[0];
        lastPressed[0] = i;

        Serial.println("NEW!");
      }
    }
  }

  Serial.print(lastPressed[0]);
  Serial.print(" + ");
  Serial.print(lastPressed[1]);
  Serial.println();

  commitButtonState = digitalRead(commitButton);
  Serial.println(commitButtonState);

  if (commitButtonState == 1) {
    Commit();
    egg += 1;
  } else {
    egg = 0;
  }

  delay(1000);
}

void Commit() {
    if (newSession == true) {
      servoBot.write(0);              // tell servo to go to position in variable 'pos'
      servoMid.write(0);
      servoTop.write(0);

      newSession = false;
      return;
    }
    
    if (lastPressed[0] != -1 || lastPressed[1] != -1) {
      Serial.println("Valid commit!");
      commits += 1;

      int valA = lastPressed[0];

      if (lastPressed[0] == -1) {
        valA = 0;
      }

      int valB = lastPressed[1];

      if (lastPressed[1] == -1) {
        valB = 0;
      }

      int rel = (valA + valB) % 2;

      if (rel == 1) {
        pos = (commits * 15) % 25;
      }
      else {
        pos = (commits * 25) % 55;
      }

      int phase = commits % 3;

      if (phase == 1) {
        servoBot.write(pos);
        botPlacement[0] = lastPressed[0];
        botPlacement[1] = lastPressed[1];
      }
      if (phase == 2) {
        servoBot.write(pos);
        servoMid.write(pos);
        midPlacement[0] = lastPressed[0];
        midPlacement[1] = lastPressed[1];
      }
      if (phase == 0) {
        servoBot.write(pos);
        servoMid.write(pos);
        servoTop.write(pos);
        topPlacement[0] = lastPressed[0];
        topPlacement[1] = lastPressed[1];
      }

      lastPressed[0] = -1;
      lastPressed[1] = -1;

      WriteData();
    }
}

void WriteData() {
  Serial.println("START");
  Serial.println(pos);

  Serial.print(botPlacement[0]);
  Serial.print(",");
  Serial.print(botPlacement[1]);
  Serial.println();

  Serial.print(midPlacement[0]);
  Serial.print(",");
  Serial.print(midPlacement[1]);
  Serial.println();

  Serial.print(topPlacement[0]);
  Serial.print(",");
  Serial.print(topPlacement[1]);
  Serial.println();

  Serial.print("END");  
}

void DoEgg() {
  int posA = servoBot.read();
  int posB = servoBot.read();
  int posC = servoTop.read();

  servoBot.write(0);
  servoMid.write(0);
  servoTop.write(0);

  for (int i = 0; i < 90; i++) {
    servoBot.write(i * 2);
    servoMid.write(i * 2);
    servoTop.write(i * 2);

    delay(300);
  }

  for (int i = 90; i > 0; i--) {
    servoBot.write(i * 2);
    servoMid.write(i * 2);
    servoTop.write(i * 2);

    delay(80);
  }

  servoBot.write(posA);
  servoMid.write(posB);
  servoTop.write(posC);
}
