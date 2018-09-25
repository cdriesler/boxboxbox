import "dart:html" as window;
import "package:angular/angular.dart";
import "package:angular_router/angular_router.dart";
import "package:angular_forms/angular_forms.dart";

import "../../formats/box.dart";
import "../../../services/box_service.dart";
import "../../../services/submission_service.dart";
//import "../services/drawing_service.dart";
import "../../route_paths.dart" as paths;

@Component(
  selector: "my-blackbox",
  templateUrl: "blackbox_component.html",
  directives: [coreDirectives, routerDirectives],
  styleUrls: ["blackbox_component.css", "canvas_styles.css"],
)
class BlackBoxComponent implements OnInit {
  List<String> allInputs = List<String>();
  List<String> boxAdverbs = List<String>();
  List<String> boxVerbs = List<String>();

  List<String> activeEndPoints = List<String>();
  List<String> activeCornerPoints = List<String>();

  String mode = "";

  String warning = "";

  String activeInput = "";
  String activeAdverb = "";
  String activeVerb = "";

  final BoxService _boxService;
  final SubmissionService _submissionService;
  //final DrawingService _drawingService;

  BlackBoxComponent(this._boxService, this._submissionService);

  //Input selection methods.
  void onInputSelect(String input) {
    activeInput = input;
  }

  void onAdverbSelect(String adverb) {
    activeAdverb = adverb;
  }

  void onVerbSelect(String verb) {
    activeVerb = verb;
  }


  //Input submission methods.
  void onSubmitInput() {
    if (activeInput == "") {
      warning = "Please select an input!";
      return;
    }

    //Show line guides.
    mode = "input";
  }

  void onCommitInput() {
    if (activeEndPoints.length < 2) {
      warning = "Please select two points.";
      return;
    }

    var div = window.document.getElementById("main-data");

    var data = "#input_line:((" + activeEndPoints[0] + "),(" + activeEndPoints[1] + "))";

    div.text = div.text + data;

    mode = "";
    activeEndPoints.clear();
    onClearSelection();

    updateCurrentData();
  }

  void onSubmitBox() {
    if (activeAdverb == "" || activeVerb == "") {
      warning = "Please select both a verb and adverb!";
      return;
    }

    //Show box guides.
    mode = "box";
  }

  void onCommitBox() {
    if (activeCornerPoints.length < 2) {
      warning = "Please select two corner points.";
      return;
    }

    var div = window.document.getElementById("main-data");

    var data = "#box_" + activeAdverb + "/" + activeVerb + ":((" + activeCornerPoints[0] + "),(" + activeCornerPoints[1] + "))";

    div.text = div.text + data;

    mode = "";
    activeCornerPoints.clear();
    onClearSelection;  
  }

  void onEndPointSelect(String coord) {

    var coords = coord.split(',');
    var coordX = coords[0];
    var coordY = coords[1];

    //Confirm points are not on the same edge.
    if (activeEndPoints.length == 1) {
      var currentCoords = activeEndPoints[0].split(',');
      var currentX = currentCoords[0];
      var currentY = currentCoords[1];

      if ((coordX == "0" && currentX == coordX) || (coordX == "6" && currentX == coordX) || (coordY == "0" && currentY == coordY) || (coordY == "6" && currentY == coordY)) {
        warning = "Please do not select points on the same edge.";
        return;
      }
    }
    if (activeEndPoints.length == 2) {
      var currentCoords = activeEndPoints[0].split(',');
      var currentX = currentCoords[0];
      var currentY = currentCoords[1];

      if ((coordX == "0" && currentX == coordX) || (coordX == "6" && currentX == coordX) || (coordY == "0" && currentY == coordY) || (coordY == "6" && currentY == coordY)) {
        warning = "Please do not select points on the same edge.";
        return;
      }
    }
    
    //Add end points to list.
    if (activeEndPoints.length < 2) {
      activeEndPoints.add(coord);
    }
    else {
      activeEndPoints[1] = activeEndPoints[0];
      activeEndPoints[0] = coord;
    }
    
  }

  void onCornerPointSelect(String coord) {

    var coords = coord.split(',');
    var coordX = coords[0];
    var coordY = coords[1];

    //Confirm points are not colinear.
    if (activeCornerPoints.length > 1) {
      var current = activeCornerPoints[0].split(',');
      var currentX = current[0];
      var currentY = current[1];

      if (coordX == currentX || coordY == currentY) {
        warning = "Please do not select colinear corner points.";
        return;
      }
    }

    //Add corner points to list.
    if (activeCornerPoints.length < 2) {
      activeCornerPoints.add(coord);
    }
    else {
      activeCornerPoints[1] = activeCornerPoints[0];
      activeCornerPoints[0] = coord;
    }

  }

  /*
  void onSelectEdge(dynamic event) {
    var div = window.document.getElementById("canvas").getBoundingClientRect();

    var point = event.page;

    if (activeLinePoints.length < 2) {
      activeLinePoints.add(point);
    }
    else {
      activeLinePoints[1] = activeLinePoints[0];
      activeLinePoints[0] = point;

      window.DivElement line = new window.DivElement();
    }

    print("Page point: " + event.page.toString());
    print("Normalized point: (" + ((event.page.x - div.topLeft.x)/div.width).toString().substring(0,4) + "," + ((event.page.y - div.topLeft.y)/div.height).toString().substring(0,4) + ")");

    //print(window.document.getElementById("canvas").getBoundingClientRect().topLeft.x.toString());
  }
  */

  //Alert methods.
  void onClearAlert() {
    warning = "";
  }

  void onClearSelection() {
    activeInput = "";
    activeVerb = "";
    activeAdverb = "";

    mode = "";
  }

  void onClearData() {
    var div = window.document.getElementById("main-data");
    div.text = "";

    var data = window.document.getElementById("current-data");
    data.innerHtml = "";
  }

  //SVG utils
  String makeLine(String coordA, String coordB) {
    var coordsA = coordA.split(',');
    var coordsB = coordB.split(',');

    var vals = List<int>();

    var x1 = int.parse(coordsA[0]) * 11;
    var y1 = int.parse(coordsA[1]) * 11;
    var x2 = int.parse(coordsB[0]) * 11;
    var y2 = int.parse(coordsB[1]) * 11;

    vals.add(x1);
    vals.add(y1);
    vals.add(x2);
    vals.add(y2);

    for (int i = 0; i < vals.length; i++) {
      if (vals[0] > 65) {
        vals[0] = 65;
      }
      if (vals[0] == 0) {
        vals[1] = 1;
      }
    }

    var svgData = "<line class='svgdata' x1='" + vals[0].toString() + "vh' y1='" + vals[1].toString() + "' x2='" + vals[2].toString() + "' y2='" + vals[3].toString() + "'/>";

    return svgData;
  }

  void updateCurrentData() {
    var current_data = window.document.getElementById("current-data");

    print(current_data.toString());
    //current_data.text = current_data.text + makeLine(activeEndPoints[0], activeEndPoints[1]);
  }

  void onDeployPayload() {
    var div = window.document.getElementById("main-data");

    //print(div.text);

    _submissionService.deployPayload(div.text);
  }

  @override
  void ngOnInit() async {
    boxAdverbs = (await _boxService.getAllAdverbs());
    boxVerbs = (await _boxService.getAllVerbs());
    //edgeGuides = (await _drawingService.getEdgeGuides());
    //innerGuides = (await _drawingService.getInnerGuides());

    allInputs.add("linear");
  }
}