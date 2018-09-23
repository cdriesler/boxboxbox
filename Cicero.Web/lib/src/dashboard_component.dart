import "package:angular/angular.dart";
import "package:angular_router/angular_router.dart";

import "./formats/project.dart";
import "../services/project_service.dart";
import "route_paths.dart" as paths;

@Component(
  selector: "my-dashboard",
  templateUrl: "dashboard_component.html",
  directives: [coreDirectives, routerDirectives],
  styleUrls: ["dashboard_component.css"],
)
class DashboardComponent {
  List<Project> projects = List<Project>();

  final ProjectService _projectService;

  DashboardComponent(this._projectService);

  String projectUrl(String id) => paths.RoutePaths.project.toUrl(parameters: {paths.idParam: "$id"});

  @override
  void ngOnInit() async {
    projects = (await _projectService.getAll()).skip(1).take(4).toList();
  }
}