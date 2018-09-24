import "dart:async";
import "package:firebase/firebase.dart" as fb;
import "package:firebase/firestore.dart" as fs;

//import "../src/formats/floor.dart";

class BoxService {

  Future<List<String>> getAllVerbs() async {
    //fs.Firestore store = fb.firestore();
    //fs.CollectionReference ref = store.collection("projects").doc(projectNumber).collection("floors");

    var allVerbs = List<String>();

    allVerbs.add("cut");
    allVerbs.add("split");

    return allVerbs;
  } 

  Future<List<String>> getAllAdverbs() async {
    var allAdverbs = List<String>();

    allAdverbs.add("sluggish");
    allAdverbs.add("inconsistent");
    allAdverbs.add("multiplicative");

    return allAdverbs;
  }

}