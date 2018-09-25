import "package:angular_router/angular_router.dart";

import "route_paths.dart";
import "dashboard_component.template.dart" as dashboard_template;
import "project_list_component.template.dart" as project_list_template;
import "project_component.template.dart" as project_template;
import "components/blackbox/blackbox_component.template.dart" as blackbox_template;

export "route_paths.dart";

class Routes {
  static final dashboard = RouteDefinition(
    routePath: RoutePaths.dashboard,
    component: dashboard_template.DashboardComponentNgFactory,
  );

  static final project = RouteDefinition(
    routePath: RoutePaths.project,
    component: project_template.ProjectComponentNgFactory,
  );

  static final projects = RouteDefinition(
    routePath: RoutePaths.projects,
    component: project_list_template.ProjectListComponentNgFactory,
  );

  static final blackbox = RouteDefinition(
    routePath: RoutePaths.blackbox,
    component: blackbox_template.BlackBoxComponentNgFactory,
  );

  static final all = <RouteDefinition>[
    project,
    projects,
    dashboard,
    blackbox,
    RouteDefinition.redirect(
      path: '',
      redirectTo: RoutePaths.dashboard.toUrl(),
    )
  ];
}